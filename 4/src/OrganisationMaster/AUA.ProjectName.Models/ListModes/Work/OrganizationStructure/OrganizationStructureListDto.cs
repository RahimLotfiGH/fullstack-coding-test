using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.Models.BaseModel.BaseDto;
using AUA.ProjectName.Models.EntitiesDto.Work;
using AutoMapper;
using AUA.ProjectName.DomainEntities.Entities.Work;

namespace AUA.ProjectName.Models.ListModes.Work.OrganizationStructure
{
    public class OrganizationStructureListDto : BaseEntityDto, IHaveCustomMappings
    {
        public int OrganizationId { get; set; }
        public OrganizationDto? OrganizationDto { get; set; }
        public string? NodeLabel { get; set; }
        public int ParentId { get; set; }

        public void ConfigureMapping(Profile configuration)
        {
            configuration.CreateMap<OrganizationStructureDto, DomainEntities.Entities.Work.OrganizationStructure>();

            configuration.CreateMap<DomainEntities.Entities.Work.OrganizationStructure, OrganizationStructureDto>()
                .ForMember(p => p.OrganizationDto, p => p.MapFrom(q => q.Organization));
        }
    }
}
