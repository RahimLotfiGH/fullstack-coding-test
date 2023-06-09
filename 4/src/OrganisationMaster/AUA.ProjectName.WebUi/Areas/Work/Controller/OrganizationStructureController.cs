using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Models.ViewModels.Work.OrganizationStructure;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.WebUi.Controllers;
using AUA.ProjectName.WebUi.Utility.Authorizations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.WebUi.Areas.Work.Controller
{
    [Area(AreasConsts.Work)]
    [WebAuthorize(EUserAccess.Profile)]
    public class OrganizationStructureController : BaseController
    {
        private readonly IOrganizationStructureService _organizationStructureService;
        private readonly IOrganizationService _organizationService;

        public OrganizationStructureController(IOrganizationStructureService organizationStructureService, IOrganizationService organizationService)
        {
            _organizationStructureService = organizationStructureService;
            _organizationService = organizationService;
        }

        public async Task<IActionResult> Index(int organizationId)
        {
            var model = await _organizationService.GetDtoByIdAsync(organizationId);

            if (model == null) return NotFound();

            return View(model);
        }

        
        public async Task<IActionResult> Insert(int organizationId)
        {
            var model = await CreateInsertStructureActionVm(organizationId);
            return View(model);
        }

        private async Task<InsertActionStructureVm> CreateInsertStructureActionVm(int organizationId)
        {
            return new InsertActionStructureVm()
            {
                OrganizationDto = await _organizationService.GetDtoByIdAsync(organizationId),
                StructureDtos = await _organizationStructureService.GetAllDto().ToListAsync()
            };
        }

        [HttpPost]
        public async Task<IActionResult> Insert(OrganizationStructureDto organizationStructureDto)
        {
            await _organizationStructureService.InsertAsync(organizationStructureDto);

            SuccessMessage(MessageConsts.InsertSuccess);

            return RedirectToAction("Index", new { organizationId=organizationStructureDto.OrganizationId });
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await CreateUpdateStructureActionVm(id);

            if(model == null) return NotFound();

            return View(model);
        }

        private async Task<UpdateActionStructureVm> CreateUpdateStructureActionVm(int organizationStructureId)
        {
            return new UpdateActionStructureVm()
            {
                OrganizationStructureDto = await _organizationStructureService.GetDtoByIdAsync(organizationStructureId),
                StructureDtos = await _organizationStructureService.GetAllDto().ToListAsync()
            };
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrganizationStructureDto organizationStructureDto)
        {
            await _organizationStructureService.UpdateAsync(organizationStructureDto);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index", new { organizationId = organizationStructureDto.OrganizationId });
        }

        public async Task<bool> _Delete(int id)
        {
            var res = await _organizationStructureService.TryDeleteAsync(id);

            SuccessMessage(MessageConsts.DeleteSuccess);

            return res;
        }
    }
}
