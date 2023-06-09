using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.DataModels.General;
using AUA.ProjectName.Models.DataModels.LoginDataModels;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.Services.GeneralService.Login.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.AppUserValidations.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.LoginModelValidations.Contracts;
using AUA.ProjectName.WebUi.Controllers;
using AUA.ProjectName.WebUi.Utility.Security;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebUi.Areas.Accounting.Controller
{
    [Area(AreasConsts.Accounting)]
    public class AccountController : BaseController
    {
        private readonly IAppUserService _appUserService;
        private readonly IUserRoleService _userRoleService;
        private readonly IAppUserInsertVmValidationService _appUserInsertVmValidationService;
        private readonly ILoginVmValidationService _loginVmValidationService;
        private readonly ILoginService _loginService;
        private readonly IAccessTokenService _accessTokenService;
        public AccountController(IAppUserService appUserService,
            IAppUserInsertVmValidationService appUserInsertVmValidationService,
            IUserRoleService userRoleService, ILoginVmValidationService loginVmValidationService,
            IAccessTokenService accessTokenService, ILoginService loginService)
        {
            _appUserService = appUserService;
            _appUserInsertVmValidationService = appUserInsertVmValidationService;
            _userRoleService = userRoleService;
            _loginVmValidationService = loginVmValidationService;
            _accessTokenService = accessTokenService;
            _loginService = loginService;
        }

        public IActionResult Register()
        {
            var model = new AppUserInsertVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AppUserInsertVm insertVm)
        {
            await ValidateInsert(insertVm);

            if (HasError) return RedirectToAction("Register");

            insertVm.IsActive = true;

            var appUserId  =await _appUserService.InsertCustomVmAsync(insertVm);

            await _userRoleService.InsertNormalUserRole(appUserId);

            SuccessMessage(MessageConsts.UserRegisterSuccess);

            return RedirectToAction("Login");
        }

        private async Task ValidateInsert(AppUserInsertVm insertVm)
        {
            var resultVm = await _appUserInsertVmValidationService.ValidationAsync(insertVm);
            AddErrors(resultVm);
        }

        public IActionResult Login()
        {
            var model = new LoginVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm loginVm)
        {
            ValidateLogin(loginVm);

            if (HasError) return RedirectToAction(nameof(Login));

            var loginDm = await DoLoginAsync(loginVm);

            if (!loginDm.IsAuthenticated)
                AddError(loginDm.ResultStatus);

            if (HasError)
                return View(loginVm);

            var resultVm = await CreateLoginResultVmAsync(loginDm);

            await UserLoginSuccessAsync(resultVm, false);

            return RedirectToAction("Index", "Profile", new {Area = "Accounting"});
        }

        private void ValidateLogin(LoginVm loginVm)
        {
            var resultVm = _loginVmValidationService.Validation(loginVm);
            AddErrors(resultVm);
        }

        private async Task<LoginDm> DoLoginAsync(LoginVm loginVm)
        {
            return await _loginService.LoginAsync(loginVm);
        }

        private async Task<UserLoggedInVm> CreateLoginResultVmAsync(LoginDm loginDm)
        {
            var accessTokenVm = await GetAccessTokenVmAsync(loginDm.UserId);

            return new UserLoggedInVm
            {
                UserId = loginDm.UserId,
                UserName = loginDm.UserName,
                FirstName = loginDm.FirstName,
                LastName = loginDm.LastName,
                RoleIds = accessTokenVm.UserRoleAccess.UserRoleIds.Distinct(),
                UserAccessIds = accessTokenVm.UserRoleAccess.UserAccessIds.Distinct(),
            };
        }
        private async Task<UserAccessTokenVm> GetAccessTokenVmAsync(long userId)
        {
            return await _accessTokenService
                .GetAccessTokenVmAsync(userId);
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
