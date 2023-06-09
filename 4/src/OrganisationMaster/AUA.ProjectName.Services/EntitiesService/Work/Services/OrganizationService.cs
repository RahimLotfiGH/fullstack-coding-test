using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ServiceInfrastructure.BaseServices;
using AutoMapper;

namespace AUA.ProjectName.Services.EntitiesService.Work.Services
{
    public class OrganizationService : GenericEntityService<Organization, OrganizationDto>, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapperInstance) : base(unitOfWork, mapperInstance)
        {
        }
    }
}
