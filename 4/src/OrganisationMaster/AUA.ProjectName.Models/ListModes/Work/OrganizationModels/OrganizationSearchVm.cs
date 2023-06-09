using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.ListModes.Work.OrganizationModels
{
    public class OrganizationSearchVm : BaseSearchVm
    {
        public string? Name { get; set; }
        public long AppUserId { get; set; }
    }
}
