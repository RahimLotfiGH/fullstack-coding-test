using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ProjectName.Services.EntitiesService.Work.Contracts;
using AUA.ServiceInfrastructure.BaseServices;
using AutoMapper;

namespace AUA.ProjectName.Services.EntitiesService.Work.Services
{
    public class OrganizationStructureService : GenericEntityService<OrganizationStructure, OrganizationStructureDto>, IOrganizationStructureService
    {
        public OrganizationStructureService(IUnitOfWork unitOfWork, IMapper mapperInstance) : base(unitOfWork, mapperInstance)
        {
        }
    }
}
