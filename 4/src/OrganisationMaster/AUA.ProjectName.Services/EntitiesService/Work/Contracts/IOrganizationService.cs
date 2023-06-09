using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AUA.ServiceInfrastructure.BaseServices;

namespace AUA.ProjectName.Services.EntitiesService.Work.Contracts
{
    public interface IOrganizationService : IGenericEntityService<Organization, OrganizationDto>
    {
    }
}
