using System.Text.Json;
using AUA.ProjectName.ApiUi.Utility;
using AUA.ProjectName.ApiUi.Utility.Security;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.DataModels.LoginDataModels;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleModels;
using AUA.ProjectName.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AUA.ProjectName.ApiUi.Areas.Accounting.Controller
{
    [Area(AreasConsts.Accounting)]
    public class AccountController : BaseController
    {
        public IActionResult Register()
        {
            var model = new AppUserInsertVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AppUserInsertVm insertVm)
        {
            insertVm.IsActive = true;

            var response = await ApiHelper.PostRequestAsync(ApiUrlConsts.AppUserInsert, insertVm);

            if (!IsSuccessful(response)) return RedirectToAction("Register");

            var userRoleVm = new UserRoleVm { AppUserId = ApiHelper.GetResultByResponse(response).GetValue<int>() };

            await ApiHelper.PostRequestAsync(ApiUrlConsts.NormalUserRoleInsert, userRoleVm);

            SuccessMessage(MessageConsts.UserRegisterSuccess);

            return RedirectToAction("Login");
        }

        private bool IsSuccessful(RestResponse response)
        {
            var isSuccess = ApiHelper.IsSuccess(response);

            if (!isSuccess)
                ErrorMessage(ApiHelper.GetErrorMessage(response));
            
            return isSuccess;
        }

        public IActionResult Login()
        {
            var model = new LoginVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm loginVm)
        {
            loginVm.ReturnUrl = String.Empty;

            var loginDm = await DoLoginAsync(loginVm);

            if (loginDm == null) return View(loginVm);

            var resultVm = await CreateLoginResultVmAsync(loginDm);

            await UserLoginSuccessAsync(resultVm, false);

            return RedirectToAction("Index", "Profile", new {Area = "Accounting"});
        }

        private async Task<LoginDm?> DoLoginAsync(LoginVm loginVm)
        {
            var response = await ApiHelper.PostRequestAsync(ApiUrlConsts.Login, loginVm);

            if (!IsSuccessful(response)) return null;

            var result = ApiHelper.GetResultByResponse(response);

            return result.Deserialize<LoginDm>(ApiHelper.GetJsonDeserializerOptions());

        }

        private async Task<UserLoggedInVm> CreateLoginResultVmAsync(LoginDm loginDm)
        {
            return new UserLoggedInVm
            {
                UserId = loginDm.UserId,
                UserName = loginDm.UserName,
                FirstName = loginDm.FirstName,
                LastName = loginDm.LastName,
                RoleIds = loginDm.RoleIds.Distinct(),
                UserAccessIds = loginDm.UserAccessIds.Cast<EUserAccess>(),
                AccessToken = loginDm.AccessToken,
                RefreshToken = loginDm.RefreshToken,
                ExpiresIn = loginDm.ExpiresIn
            };
        }

        private async Task UserLoginSuccessAsync(UserLoggedInVm userLoggedInVm, bool rememberMe)
        {
            await SecurityHelper
                .UserLoginSuccessAsync(HttpContext, userLoggedInVm, rememberMe);

        }

        public async Task<IActionResult> LogoutAsync()
        {
            await SecurityHelper.LogoffAsync(HttpContext);

            return RedirectToAction("Login");
        }
    }
}
