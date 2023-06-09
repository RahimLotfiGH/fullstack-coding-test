using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.WebUi.Controllers;
using AUA.ProjectName.WebUi.Utility.Authorizations;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebUi.Areas.Work.Controller
{
    [Area(AreasConsts.Work)]
    [WebAuthorize(EUserAccess.Profile)]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public IActionResult Index()
        {
            var model = _organizationService.GetAllDto().Where(p => p.AppUserId == CurrentUserId);
            return View(model);
        }

        public IActionResult Insert()
        {
            var model = new OrganizationDto();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync(OrganizationDto organizationDto)
        {
            organizationDto.AppUserId = CurrentUserId;

            await _organizationService.InsertAsync(organizationDto);

            SuccessMessage(MessageConsts.InsertSuccess);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _organizationService.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(OrganizationDto organizationDto)
        {
            organizationDto.AppUserId = CurrentUserId;

            await _organizationService.UpdateAsync(organizationDto);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index");
        }

        public async Task<bool> _Delete(int id)
        {
            var res = await _organizationService.TryDeleteAsync(id);

            SuccessMessage(MessageConsts.DeleteSuccess);

            return res;
        }
    }
}
