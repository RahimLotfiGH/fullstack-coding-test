using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ListModes.Work.OrganizationModels;
using AUA.ProjectName.Models.ListModes.Work.OrganizationStructure;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ProjectName.Services.ListService.Work.Contracts;
using AUA.ServiceInfrastructure.BaseSearchService;
using AutoMapper;

namespace AUA.ProjectName.Services.ListService.Work.Services
{
    public class OrganizationStructureListService : BaseListService<OrganizationStructure, OrganizationStructureListDto, OrganizationStructureSearchVm>, IOrganizationStructureListService
    {
        public OrganizationStructureListService(IUnitOfWork unitOfWork, IMapper mapperInstance) : base(unitOfWork, mapperInstance)
        {
        }

        public async Task<ListResultVm<OrganizationStructureListDto>> ListAsync(OrganizationStructureSearchVm organizationStructureSearchVm)
        {
            SetSearchVm(organizationStructureSearchVm);

            ApplyNodeLabelFilter();
            ApplyOrganizationIdFilter();

            return await CreateListVmResultAsync();
        }

        private void ApplyNodeLabelFilter()
        {
            if (SearchVm.NodeLabel is null)
                return;

            Query = Query.Where(p => p.NodeLabel.Contains(SearchVm.NodeLabel));
        }

        private void ApplyOrganizationIdFilter()
        {
            if (SearchVm.OrganizationId == 0)
                return;

            Query = Query.Where(p => p.OrganizationId == SearchVm.OrganizationId);
        }
    }

}
