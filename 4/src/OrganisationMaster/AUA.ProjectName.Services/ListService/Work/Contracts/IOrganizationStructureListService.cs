using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ListModes.Work.OrganizationStructure;
using AUA.ServiceInfrastructure.BaseSearchService;

namespace AUA.ProjectName.Services.ListService.Work.Contracts
{
    public interface IOrganizationStructureListService : IBaseListService<OrganizationStructure, OrganizationStructureListDto>
    {
        Task<ListResultVm<OrganizationStructureListDto>> ListAsync(
            OrganizationStructureSearchVm organizationStructureSearchVm);
    }
}
