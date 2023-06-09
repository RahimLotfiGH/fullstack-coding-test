using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Models.ListModes.Work.OrganizationModels;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.Services.ListService.Work.Contracts;
using AUA.ProjectName.WebApi.Controllers;
using AUA.ProjectName.WebApi.Utility.ApiAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.WebApi.Areas.Work.Controllers
{
    public class OrganizationController : BaseApiController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationListService _organizationListService;

        public OrganizationController(IOrganizationService organizationService, IOrganizationListService organizationListService)
        {
            _organizationService = organizationService;
            _organizationListService = organizationListService;
        }

        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<ListResultVm<OrganizationListDto>>> ListAsync(OrganizationSearchVm searchVm)
        {
            var result = await _organizationListService.ListAsync(searchVm);

            return CreateSuccessResult(result);
        }

        [HttpGet]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<dynamic> GetCurrentUserOrgListAsync()
        {
            var result = await _organizationService.GetAllDto().Where(p => p.AppUserId == UserId).ToListAsync();

            if (result.Count == 0)
                return CreateInvalidResult(EResultStatus.IsEmpty);

            return CreateSuccessResult(result);
        }


        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<int>> InsertAsync(OrganizationListDto dto)
        {
            dto.AppUserId = UserId;

            var id = await _organizationService.InsertCustomVmAsync(dto);

            return id == 0 ?
                CreateInvalidResult<int>(EResultStatus.ErrorOperations) :
                CreateSuccessResult(id);
        }

        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<bool>> UpdateAsync(OrganizationListDto organizationDto)
        {
            organizationDto.AppUserId = UserId;

            var result = await _organizationService.UpdateCustomVmAsync(organizationDto);

            return result ?
                CreateSuccessResult(true) :
                CreateInvalidResult<bool>();
        }


        [HttpPost]
        [WebApiAuthorize(EUserAccess.Profile)]
        public async Task<ResultModel<bool>> DeleteAsync(OrganizationListDto organizationDto)
        {
            var isDeleted = await _organizationService.TryDeleteAsync(organizationDto.Id);

            return isDeleted ?
                CreateSuccessResult(true) :
                CreateInvalidResult<bool>(EResultStatus.ErrorOperations);
        }


    }
}
