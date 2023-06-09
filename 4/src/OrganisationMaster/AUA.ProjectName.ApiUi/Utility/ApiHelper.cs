using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AUA.ProjectName.ApiUi.Utility.Security;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Tools.Config.JsonSetting;
using RestSharp;

namespace AUA.ProjectName.ApiUi.Utility
{
    public class ApiHelper
    {

        public static async Task<RestResponse> PostRequestAsync(string apiAbsoluteUrl, object body)
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + apiAbsoluteUrl;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);

            request.AddBody(body);

            return await client.ExecutePostAsync(request);

        }

        public static async Task<RestResponse> PostRequestWithTokenAsync(string apiAbsoluteUrl,
            string authorizationToken, object body)
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + apiAbsoluteUrl;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);

            request.AddHeader("AuthorizationToken", authorizationToken);

            request.AddBody(body);

            return await client.ExecutePostAsync(request);

        }

        public static async Task<RestResponse> PutRequestWithTokenAsync(string apiAbsoluteUrl,
            string authorizationToken, object body)
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + apiAbsoluteUrl;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Put);

            request.AddHeader("AuthorizationToken", authorizationToken);

            request.AddBody(body);

            return await client.ExecutePutAsync(request);

        }

        public static async Task<RestResponse> DeleteRequestWithTokenAsync(string apiAbsoluteUrl,
            string authorizationToken, object body)
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + apiAbsoluteUrl;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Delete);

            request.AddHeader("AuthorizationToken", authorizationToken);

            request.AddBody(body);

            return await client.ExecuteAsync(request);

        }


        public static async Task<RestResponse> GetRequestWithTokenAsync(string apiAbsoluteUrl,
            string authorizationToken)
        {
            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + apiAbsoluteUrl;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Get);

            request.AddHeader("AuthorizationToken", authorizationToken);

            return await client.ExecuteGetAsync(request);

        }

        public static RestResponse RefreshToken(HttpContext httpContext)
        {
            var loggedInVm = SecurityHelper.GetUserLoggedInVm(httpContext);

            var url = Common.Tools.Config.JsonSetting.AppConfiguration.ApiUrlString + ApiUrlConsts.RefreshToken;

            var client = new RestClient(url);

            var request = new RestRequest(url, Method.Post);

            request.AddBody(new
            {
                appTypeCode = 1,
                accessToken = loggedInVm.AccessToken,
                refreshToken = loggedInVm.RefreshToken
            });

            return client.ExecutePost(request);

        }


        public static JsonNode? GetResultByResponse(RestResponse response)
        {
            return JsonSerializer.Deserialize<JsonNode>(response.Content!)![ResponseConsts.Result];
        }

        public static JsonNode? GetContentByResponse(RestResponse response)
        {
            return JsonSerializer.Deserialize<JsonNode>(response.Content!)!;
        }


        public static bool IsSuccess(RestResponse response)
        {
            var content = GetContentByResponse(response);

            return content[ResponseConsts.IsSuccess].GetValue<bool>();
        }

        public static string? GetErrorMessage(RestResponse response)
        {
            var content = GetContentByResponse(response).AsObject();

            var errors = content[ResponseConsts.Errors].AsArray();

            return errors[0][ResponseConsts.ErrorMessage].ToString();
        }

        public static JsonSerializerOptions GetJsonDeserializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

    }
}