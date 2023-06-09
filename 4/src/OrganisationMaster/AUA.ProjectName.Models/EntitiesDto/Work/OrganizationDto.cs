using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseDto;
using AUA.ProjectName.Models.ListModes.Work.OrganizationModels;
using AutoMapper;

namespace AUA.ProjectName.Models.EntitiesDto.Work
{
    public class OrganizationDto : BaseEntityDto, IHaveCustomMappings
    {
        public string? Name { get; set; }
        public long AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<OrganizationStructureDto> OrganizationStructuresDto { get; set; }
        public void ConfigureMapping(Profile configuration)
        {
            configuration.CreateMap<OrganizationDto, Organization>();

            configuration.CreateMap<Organization, OrganizationDto>()
                .ForMember(p => p.OrganizationStructuresDto, p => p.MapFrom(q => q.OrganizationStructures));
        }
    }
}
