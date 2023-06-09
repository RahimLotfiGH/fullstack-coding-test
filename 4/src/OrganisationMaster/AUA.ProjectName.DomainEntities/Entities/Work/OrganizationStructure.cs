using AUA.ProjectName.DomainEntities.BaseEntities;

namespace AUA.ProjectName.DomainEntities.Entities.Work
{
    public class OrganizationStructure : DomainEntity
    {
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public string? NodeLabel { get; set; }
        public int ParentId { get; set; }
    }
}
