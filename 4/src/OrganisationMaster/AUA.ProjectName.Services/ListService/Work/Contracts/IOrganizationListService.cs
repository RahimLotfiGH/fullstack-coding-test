using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Models.ListModes.Accounting.AppUserModels;
using AUA.ProjectName.Models.ListModes.Work.OrganizationModels;
using AUA.ServiceInfrastructure.BaseSearchService;

namespace AUA.ProjectName.Services.ListService.Work.Contracts
{
    public interface IOrganizationListService : IBaseListService<Organization, OrganizationListDto>
    {
        Task<ListResultVm<OrganizationListDto>> ListAsync(OrganizationSearchVm organizationSearchVm);
    }
}
