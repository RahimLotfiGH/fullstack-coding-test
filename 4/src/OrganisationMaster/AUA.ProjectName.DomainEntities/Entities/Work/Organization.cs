using AUA.ProjectName.DomainEntities.BaseEntities;
using AUA.ProjectName.DomainEntities.Entities.Accounting;

namespace AUA.ProjectName.DomainEntities.Entities.Work
{
    public class Organization : DomainEntity
    {
        public string? Name { get; set; }
        public long AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<OrganizationStructure> OrganizationStructures { get; set; }
    }
}
