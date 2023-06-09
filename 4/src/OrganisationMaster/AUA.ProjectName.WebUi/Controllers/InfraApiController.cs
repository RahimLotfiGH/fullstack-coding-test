using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Common.Tools.Security;
using AUA.ProjectName.Models.BaseModel.BaseValidationModels;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using AUA.ProjectName.WebUi.Utility.Security;
using Hil.VideoShop.Common.Consts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebUi.Controllers
{


    public class InfraController : BaseValidationController
    {
        protected ValidationResultVm ValidationResultVm;
        private UserLoggedInVm _accessTokenDataVm;

        protected InfraController()
        {
            ValidationResultVm ??= new ValidationResultVm();
        }

        //protected static ResultModel<TResultModel> CreateInvalidResult<TResultModel>(EResultStatus eResultStatus)
        //{
        //    return new()
        //    {
        //        Errors = new List<ErrorVm>
        //        {
        //            new()
        //            {
        //                ErrorType = ELogType.Danger,
        //                ErrorMessage =  eResultStatus.ToDescription(),
        //                }
        //        }

        //    };
        //}
        protected void AddSession(string key, object value)
        {
            HttpContext.Session.SetString(key, value.ObjectSerialize());
        }
        protected void AddSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        protected T GetSession<T>(string key) where T : new()
        {
            var examSessionToken = HttpContext.Session.GetString(key);

            return !string.IsNullOrWhiteSpace(examSessionToken) ?
                examSessionToken.ObjectDeserialize<T>() : new T();
        }


        protected JsonResult CreateResult(object data)
        {
            return new(data);
        }



        //protected static ResultModel<TResultModel> CreateSuccessResult<TResultModel>(TResultModel resultModel)
        //{
        //    return new ResultModel<TResultModel>
        //    {
        //        Result = resultModel,
        //    };
        //}



        private UserLoggedInVm GetAccessTokenDataVm()
        {
            return _accessTokenDataVm ??= SecurityHelper.GetUserLoggedInVm(HttpContext); ;
        }

        //protected ResultModel<EResultStatus> CreateResult(EResultStatus result)
        //{
        //    return IsSuccess(result) ?
        //           CreateSuccessResult(result) :
        //           CreateInvalidResult<EResultStatus>(result);
        //}
        //protected bool IsSuccess(EResultStatus status)
        //{
        //    return status == EResultStatus.Success;
        //}

        protected static Exception GetException(HttpContext context)
        {
            var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exHandlerFeature?.Error;

            return exception;
        }

        protected long CurrentUserId => GetAccessTokenDataVm() is null ?
                                 AppConsts.SystemUserId :
                                 GetAccessTokenDataVm().UserId;

        protected string? CurrentUserName => GetAccessTokenDataVm() is null ?
                                            string.Empty :
                                            GetAccessTokenDataVm().UserName;

        protected IEnumerable<EUserAccess>? CurrentUserAccessIds => GetAccessTokenDataVm() is null ?
                                            new List<EUserAccess>() :
                                            GetAccessTokenDataVm().UserAccessIds;

        protected IEnumerable<int>? CurrentUserRoleIds => GetAccessTokenDataVm() is null ?
                                                         new List<int>() :
                                                         GetAccessTokenDataVm().RoleIds;

        protected string? CurrentUserFirstName => GetAccessTokenDataVm() is null ?
                                                 string.Empty :
                                                  GetAccessTokenDataVm().FirstName;

        protected string? CurrentUserLastName => GetAccessTokenDataVm() is null ?
                                                string.Empty :
                                                 GetAccessTokenDataVm().LastName;

        protected string CurrentUserFullName => CurrentUserFirstName + " " + CurrentUserLastName;


        protected FileResult ExportExcelFile(MemoryStream fileStream, string fileName = FileInfoConsts.DefaultExcelFileName)
        {
            if (NoExtension(fileName))
                fileName += FileInfoConsts.ExcelExtension;

            return File(fileStream, FileInfoConsts.ExcelContent, fileName);
        }

        private static bool NoExtension(string fileName) => string.IsNullOrWhiteSpace(Path.GetExtension(fileName));

    }
}
