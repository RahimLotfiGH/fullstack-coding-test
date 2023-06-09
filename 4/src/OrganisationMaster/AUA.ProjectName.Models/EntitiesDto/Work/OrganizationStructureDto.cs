using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.DomainEntities.Entities.Work;
using AUA.ProjectName.Models.BaseModel.BaseDto;
using AutoMapper;

namespace AUA.ProjectName.Models.EntitiesDto.Work
{
    public class OrganizationStructureDto : BaseEntityDto, IHaveCustomMappings
    {
        public int OrganizationId { get; set; }
        public OrganizationDto? OrganizationDto { get; set; }
        public string? NodeLabel { get; set; }
        public int ParentId { get; set; }

        public void ConfigureMapping(Profile configuration)
        {
            configuration.CreateMap<OrganizationStructureDto, OrganizationStructure>();

            configuration.CreateMap<OrganizationStructure, OrganizationStructureDto>()
                .ForMember(p => p.OrganizationDto, p => p.MapFrom(q => q.Organization));
        }
    }
}
