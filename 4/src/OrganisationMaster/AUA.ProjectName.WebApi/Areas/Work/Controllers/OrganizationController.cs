using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ListModes.Work.OrganizationStructure;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.Services.ListService.Work.Contracts;
using AUA.ProjectName.WebApi.Controllers;
using AUA.ProjectName.WebApi.Utility.ApiAuthorization;
using Microsoft.AspNetCore.Mvc;

namespace AUA.ProjectName.WebApi.Areas.Work.Controllers
{
    public class OrganizationStructureController : BaseApiController
    {
        private readonly IOrganizationStructureService _organizationStructureService;
        private readonly IOrganizationStructureListService _organizationStructureListService;

        public OrganizationStructureController(IOrganizationStructureService organizationStructureService, IOrganizationStructureListService organizationStructureListService)
        {
            _organizationStructureService = organizationStructureService;
            _organizationStructureListService = organizationStructureListService;
        }

        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<dynamic> ListAsync(OrganizationStructureSearchVm searchVm)
        {
            var result = await _organizationStructureListService.ListAsync(searchVm);

            if (!result.ResultVms.Any())
            {
                return CreateInvalidResult(EResultStatus.IsEmpty);
            }

            return CreateSuccessResult(result);
        }
        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<dynamic> GetOrganizationStructureById(OrganizationStructureListDto organizationStructureListDto)
        {
            var result = await _organizationStructureService.GetDtoByIdAsync(organizationStructureListDto.Id);

            return CreateSuccessResult(result);
        }

        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<int>> InsertAsync(OrganizationStructureListDto dto)
        {
            var id = await _organizationStructureService.InsertCustomVmAsync(dto);

            return id == 0 ?
                CreateInvalidResult<int>(EResultStatus.ErrorOperations) :
                CreateSuccessResult(id);
        }

        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<bool>> UpdateAsync(OrganizationStructureListDto organizationStructureListDto)
        {
            var result = await _organizationStructureService.UpdateCustomVmAsync(organizationStructureListDto);

            return result ?
                CreateSuccessResult(true) :
                CreateInvalidResult<bool>();
        }


        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<bool>> DeleteAsync(OrganizationStructureListDto organizationStructureListDto)
        {
            var isDeleted = await _organizationStructureService.TryDeleteAsync(organizationStructureListDto.Id);

            return isDeleted ?
                CreateSuccessResult(true) :
                CreateInvalidResult<bool>(EResultStatus.ErrorOperations);
        }


    }
}
