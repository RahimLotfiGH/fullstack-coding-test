using AUA.ProjectName.Models.EntitiesDto.Work;

namespace AUA.ProjectName.Models.ViewModels.Work.OrganizationStructure
{
    public class UpdateActionStructureVm
    {
        public ICollection<OrganizationStructureDto> StructureDtos { get; set; }
        public OrganizationStructureDto OrganizationStructureDto { get; set; }
    }
}
