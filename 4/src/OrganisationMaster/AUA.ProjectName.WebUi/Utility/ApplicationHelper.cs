﻿using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Common.Tools.Config.JsonSetting;
using AUA.ProjectName.Common.Tools.Security;
using AUA.ProjectName.Models.BaseModel.BaseValidationModels;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.GeneralModels.AccessTokenModels;
using AUA.ProjectName.Models.GeneralModels.GeneralVm;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace AUA.ProjectName.WebUi.Utility
{
    public static class ApplicationHelper
    {

        public static IActionResult CreateResult(EResultStatus resultStatus)
        {
            return new RedirectResult("/Home/ErrorPage?resultStatus=" + (int)resultStatus);
        }


        public static AccessTokenDataVm GetCurrentUserDataVm(HttpContext context)
        {
            var guidAccessToken = GetAuthorizationToken(context);

            if (IsNullOrWhiteSpace(guidAccessToken))
                return null;

            var jsonAccessToken = EncryptionHelper.AesDecryptString(guidAccessToken);

            if (IsNullOrEmpty(jsonAccessToken))
                return null;

            var accessTokenDataVm = jsonAccessToken.ObjectDeserialize<AccessTokenDataVm>();

            return accessTokenDataVm;
        }

        public static string GetAuthorizationToken(HttpContext context)
        {
            context
                .Request
                .Headers
                .TryGetValue(AppConsts.AuthorizationAccessTokenName, out var accessToken);

            return IsNullOrWhiteSpace(accessToken.ToString()) ?
                    Empty :
                    (string)accessToken;
        }

        public static ResultModel<BaseViewModel> ResultHandler(EResultStatus resultStatus)
        {
            var resultViewModel = new ResultModel<BaseViewModel>
            {
                Errors = new List<ErrorVm>
                {
                    new()
                    {
                       ErrorMessage = resultStatus.ToDescription()
                    }
                }

            };


            return resultViewModel;
        }

        public static string GetAccessTokenInMemory(string tokenKeyGuid)
        {
            //var inMemoryTokenService = ServiceFactory.GetService<InMemoryService>();


            //return inMemoryTokenService.GetAccessToken(tokenKeyGuid);
            return Empty;
        }


        public static string DecryptString(string encryptText)
        {
            return EncryptionHelper
                    .AesDecryptString(
                    encryptText,
                    AppSetting.DataEncryptionKey);
        }

    }
}
