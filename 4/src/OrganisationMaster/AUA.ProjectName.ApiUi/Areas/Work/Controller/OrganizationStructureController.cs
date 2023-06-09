using System.Text.Json;
using AUA.ProjectName.ApiUi.Utility;
using AUA.ProjectName.ApiUi.Utility.Authorizations;
using AUA.ProjectName.ApiUi.Utility.Security;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Models.ViewModels.Work.OrganizationStructure;
using AUA.ProjectName.WebUi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.ApiUi.Areas.Work.Controller
{
    [Area(AreasConsts.Work)]
    [WebAuthorize(EUserAccess.Profile)]
    public class OrganizationStructureController : BaseController
    {
        public async Task<IActionResult> Index(int organizationId)
        {
            var model = await GetOrganizationDtoByIdAsync(organizationId);

            if (model == null) return NotFound();

            return View(model);
        }

        private async Task<OrganizationDto[]?> GetOrganizationDtosAsync()
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.GetRequestWithTokenAsync(ApiUrlConsts.GetCurrentUserOrganizationList, token);

            var result = ApiHelper.GetResultByResponse(response);

            if (ApiHelper.IsSuccess(response))
            {
                return result.Deserialize<OrganizationDto[]>(ApiHelper.GetJsonDeserializerOptions());
            }

            return Array.Empty<OrganizationDto>();
        }

        private async Task<OrganizationDto?> GetOrganizationDtoByIdAsync(int organizationId)
        {
            var organizationDtos = await GetOrganizationDtosAsync();

            return organizationDtos.FirstOrDefault(p => p.Id == organizationId);
        }

        public async Task<IActionResult> Insert(int organizationId)
        {
            var model = await CreateInsertStructureActionVm(organizationId);
            return View(model);
        }

        private async Task<InsertActionStructureVm> CreateInsertStructureActionVm(int organizationId)
        {
            var organizationDto = await GetOrganizationDtoByIdAsync(organizationId);
            return new InsertActionStructureVm()
            {
                OrganizationDto = organizationDto,
                StructureDtos = organizationDto.OrganizationStructuresDto
            };
        }

        [HttpPost]
        public async Task<IActionResult> Insert(OrganizationStructureDto organizationStructureDto)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationStructureInsert, token,
                organizationStructureDto);

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
            var organizationStructureDto = await GetOrganizationStructureDtoById(organizationStructureId);
            var organizationDto = await GetOrganizationDtoByIdAsync(organizationStructureDto.OrganizationId);
            return new UpdateActionStructureVm
            {
                OrganizationStructureDto = organizationStructureDto,
                StructureDtos = organizationDto.OrganizationStructuresDto
                
            };
        }

        private async Task<OrganizationStructureDto> GetOrganizationStructureDtoById(int id)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            var response = await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.GetOrganizationStructureById, token,
                new OrganizationStructureDto() { Id = id });

            return ApiHelper.GetResultByResponse(response).Deserialize<OrganizationStructureDto>(ApiHelper.GetJsonDeserializerOptions());
        }
        [HttpPost]
        public async Task<IActionResult> Update(OrganizationStructureDto organizationStructureDto)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationStructureUpdate, token, organizationStructureDto);

            SuccessMessage(MessageConsts.UpdateSuccess);

            return RedirectToAction("Index", new { organizationId = organizationStructureDto.OrganizationId });
        }

        public async Task<bool> _Delete(int id)
        {
            var token = SecurityHelper.GetUserLoggedInVm(HttpContext).AccessToken;

            await ApiHelper.PostRequestWithTokenAsync(ApiUrlConsts.OrganizationStructureDelete, token, new OrganizationStructureDto(){Id = id});

            SuccessMessage(MessageConsts.DeleteSuccess);

            return true;
        }
    }
}
