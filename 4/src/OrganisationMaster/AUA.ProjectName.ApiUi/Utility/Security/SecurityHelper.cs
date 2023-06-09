using System.Security.Claims;
using System.Security.Cryptography;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Common.Tools.Security;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RestSharp;
using AUA.ProjectName.Common.Tools.Config.JsonSetting;
using static System.String;
using System.Net;
using System.Text.Json;
using AUA.ProjectName.Common.Enums;
using NuGet.Protocol;
using System.Text.Json.Nodes;

namespace AUA.ProjectName.ApiUi.Utility.Security
{
    public static class SecurityHelper
    {
        public static bool IsLoggedIn(HttpContext context) => IsAuthenticated(context);

        private static bool IsAuthenticated(HttpContext context) => context.User.Identity != null &&
                                                                    context.User.Identity.IsAuthenticated;


        public static async Task UserLoginSuccessAsync(HttpContext context, UserLoggedInVm userLoggedInVm, bool rememberMe)
        {
            var userData = userLoggedInVm.ObjectSerialize();

            userData = EncryptionHelper.AesEncryptString(userData);

            await CreateFormsAuthenticationTicketAsync(rememberMe, userData, context);

        }

        public static void UserLoginSuccess(HttpContext context, UserLoggedInVm userLoggedInVm, bool rememberMe)
        {
            var userData = userLoggedInVm.ObjectSerialize();

            userData = EncryptionHelper.AesEncryptString(userData);

            CreateFormsAuthenticationTicketAsync(rememberMe, userData, context);

        }

        public static UserLoggedInVm GetUserLoggedInVm(HttpContext context)
        {
            if (!IsLoggedIn(context))
                return new UserLoggedInVm();

            var userData = GetClimesData(context);

            if (IsNullOrWhiteSpace(userData))
                userData = GetUserDataFromContext(context);

            if (!IsNullOrEmpty(userData))
                userData = DecryptString(userData);


            return IsNullOrWhiteSpace(userData) ?
                   new UserLoggedInVm() :
                   userData.ObjectDeserialize<UserLoggedInVm>();

        }

        private static string DecryptString(string value)
        {
            return EncryptionHelper.AesDecryptString(value);
        }

        private static string? GetUserDataFromContext(HttpContext context)
        {
            return context
                      .Items[AppConsts.UserDataClimeName]?
                      .ToString();
        }

        public static string GetSha512Hash(string value)
        {
            return EncryptionHelper.GetSha512Hash(value);
        }

        private static string GetClimesData(HttpContext context)
        {
            return (from c in context.User.Claims
                    where c.Type == AppConsts.UserDataClimeName
                    select c.Value)
                    .FirstOrDefault()!;
        }

        private static async Task CreateFormsAuthenticationTicketAsync(bool rememberMe, string userData, HttpContext context)
        {
            var expireDate = GetExpireDate(rememberMe);

            var claimsIdentity = GetClaimsIdentity(userData);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = expireDate
                });

        }

        private static ClaimsIdentity GetClaimsIdentity(string userData)
        {
            var claims = GetClaims(userData);

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        }

        private static IEnumerable<Claim> GetClaims(string userData)
        {
            return new List<Claim>
            {
                new Claim(AppConsts.UserDataClimeName,userData )
            };
        }

        private static DateTime GetExpireDate(bool rememberMe)
        {
            return rememberMe ? DateTime.UtcNow.AddDays(30) :
                                DateTime.UtcNow.AddDays(1);
        }


        public static async Task LogoffAsync(HttpContext context)
        {
            context.Session.Clear();

            await context
                     .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static void Logoff(HttpContext context)
        {
            context.Session.Clear();

            context
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        public static Guid CreateCryptographicallySecureGuid()
        {
            var rand = RandomNumberGenerator.Create();

            var bytes = new byte[16];
            rand.GetBytes(bytes);
            return new Guid(bytes);
        }

        public static string GetHashGuid()
        {
            return EncryptionHelper
                .GetSha256Hash(CreateCryptographicallySecureGuid().ToString());
        }

        public static string? GetSystemAccessToken()
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + ApiUrlConsts.Login;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);

            var adminCredentials = GetAdminCredentials();

            request.AddBody(adminCredentials);

            var response = client.ExecutePost(request);

            if (!response.IsSuccessful) return null;

            var result = JsonSerializer.Deserialize<JsonNode>(response.Content!)[ResponseConsts.Result];

            return result[ResponseConsts.AccessToken].ToString();
        }

        private static LoginVm GetAdminCredentials()
        {
            return new LoginVm()
            {
                UserName = "Admin",
                Password = Common.Tools.Config.JsonSetting.AppConfiguration.DefaultPassword,
                ReturnUrl = String.Empty
            };
        }
    }
}