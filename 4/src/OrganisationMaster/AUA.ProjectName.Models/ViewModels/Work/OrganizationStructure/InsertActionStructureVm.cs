using AUA.ProjectName.Models.EntitiesDto.Work;

namespace AUA.ProjectName.Models.ViewModels.Work.OrganizationStructure
{
    public class InsertActionStructureVm
    {
        public ICollection<OrganizationStructureDto> StructureDtos { get; set; }
        public OrganizationDto OrganizationDto { get; set; }
    }
}
