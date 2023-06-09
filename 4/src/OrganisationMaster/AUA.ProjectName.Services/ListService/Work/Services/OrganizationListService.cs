using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Models.ListModes.Work.OrganizationModels;
using AUA.ProjectName.Services.ListService.Work.Contracts;
using AUA.ServiceInfrastructure.BaseSearchService;
using AutoMapper;

namespace AUA.ProjectName.Services.ListService.Work.Services
{
    public class OrganizationListService : BaseListService<Organization, OrganizationListDto, OrganizationSearchVm>, IOrganizationListService
    {
        public OrganizationListService(IUnitOfWork unitOfWork, IMapper mapperInstance) : base(unitOfWork, mapperInstance)
        {
        }

        public async Task<ListResultVm<OrganizationListDto>> ListAsync(OrganizationSearchVm organizationSearchVm)
        {
            SetSearchVm(organizationSearchVm);

            ApplyNameFilter();
            ApplyAppUserIdFilter();

            return await CreateListVmResultAsync();
        }

        private void ApplyNameFilter()
        {
            if (SearchVm.Name is null)
                return;

            Query = Query.Where(p => p.Name.Contains(SearchVm.Name));
        }

        private void ApplyAppUserIdFilter()
        {
            if (SearchVm.AppUserId == 0)
                return;

            Query = Query.Where(p => p.AppUserId == SearchVm.AppUserId);
        }
    }
}
