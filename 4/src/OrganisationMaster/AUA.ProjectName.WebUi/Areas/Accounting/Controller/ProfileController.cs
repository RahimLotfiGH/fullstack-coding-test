using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.AppUserValidations.Contracts;
using AUA.ProjectName.WebUi.Controllers;
using AUA.ProjectName.WebUi.Utility.Authorizations;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebUi.Areas.Accounting.Controller
{
    [WebAuthorize(EUserAccess.Profile)]
    [Area(AreasConsts.Accounting)]
    public class ProfileController : BaseController
    {
        private readonly IAppUserService _appUserService;
        private readonly IAppUserUpdateVmValidationService _appUserUpdateVmValidationService;
        private readonly IChangePasswordValidationService _changePasswordValidationService;

        public ProfileController(IAppUserService appUserService,
            IAppUserUpdateVmValidationService appUserUpdateVmValidationService, IChangePasswordValidationService changePasswordValidationService)
        {
            _appUserService = appUserService;
            _appUserUpdateVmValidationService = appUserUpdateVmValidationService;
            _changePasswordValidationService = changePasswordValidationService;
        }

        public IActionResult Index()
        {
            var model = GetLoggedInUser();
            return View(model);
        }

        private AppUserDto GetLoggedInUser()
        {
            return _appUserService.GetDtoById(CurrentUserId);
        }

        public async Task<IActionResult> UpdateAsync(AppUserUpdateVm appUserUpdateVm)
        {
            appUserUpdateVm.Id = CurrentUserId;

            appUserUpdateVm.IsActive = true;

            await ValidateUpdate(appUserUpdateVm);

            if (HasError) return RedirectToAction("Index");

            await _appUserService.UpdateCustomVmAsync(appUserUpdateVm);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

        private async Task ValidateUpdate(AppUserUpdateVm appUserUpdateVm)
        {
            var resultVm = await _appUserUpdateVmValidationService.ValidationAsync(appUserUpdateVm);
            AddErrors(resultVm);
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordVm changePasswordVm)
        {
            await ValidateChangePassword(changePasswordVm);

            if (HasError) return RedirectToAction("Index");

            await _appUserService.ChangePasswordAsync(changePasswordVm.NewPassword, CurrentUserId);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

        private async Task ValidateChangePassword(ChangePasswordVm changePasswordVm)
        {
            var resultVm = await _changePasswordValidationService.ChangePasswordValidationAsync(changePasswordVm, CurrentUserId);
            AddErrors(resultVm);
        }
    }
}
