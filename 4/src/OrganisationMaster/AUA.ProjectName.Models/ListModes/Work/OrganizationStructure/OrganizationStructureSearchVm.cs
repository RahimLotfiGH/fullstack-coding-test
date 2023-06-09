using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.ListModes.Work.OrganizationStructure
{
    public class OrganizationStructureSearchVm : BaseSearchVm
    {
        public string? NodeLabel { get; set; }
        public int OrganizationId { get; set; }
    }
}
