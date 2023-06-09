using System.Text.Json;
using AUA.ProjectName.ApiUi.Utility;
using AUA.ProjectName.ApiUi.Utility.Authorizations;
using AUA.ProjectName.ApiUi.Utility.Security;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AUA.ProjectName.ApiUi.Areas.Accounting.Controller
{
    [WebAuthorize(EUserAccess.Profile)]
    [Area(AreasConsts.Accounting)]
    public class ProfileController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var model = await GetLoggedInUserAsync();
            return View(model);
        }

        private async Task<AppUserDto?> GetLoggedInUserAsync()
        {
            var response = await ApiHelper.GetRequestWithTokenAsync(ApiUrlConsts.GetCurrentAppUser,
                SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken);

            var a = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            return ApiHelper.GetResultByResponse(response)
                .Deserialize<AppUserDto>(ApiHelper.GetJsonDeserializerOptions());
        }

        public async Task<IActionResult> UpdateAsync(AppUserUpdateVm appUserUpdateVm)
        {
            appUserUpdateVm.Id = CurrentUserId;

            appUserUpdateVm.IsActive = true;

            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.AppUserCurrentUserUpdate, token, appUserUpdateVm);

            if (!IsSuccessful(response)) return RedirectToAction("Index");

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

        private bool IsSuccessful(RestResponse response)
        {
            var isSuccess = ApiHelper.IsSuccess(response);

            if (!isSuccess)
                ErrorMessage(ApiHelper.GetErrorMessage(response));

            return isSuccess;
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordVm changePasswordVm)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.PutRequestWithTokenAsync(ApiUrlConsts.AppUserChangePassword, token, changePasswordVm);

            if (!IsSuccessful(response)) return RedirectToAction("Index");

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

    }
}
