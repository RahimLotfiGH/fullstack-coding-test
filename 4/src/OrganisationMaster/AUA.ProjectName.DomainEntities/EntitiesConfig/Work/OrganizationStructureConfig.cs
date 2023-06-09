using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.DomainEntities.Entities.Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUA.ProjectName.DomainEntities.EntitiesConfig.Work
{
    public class OrganizationStructureConfig : IEntityTypeConfiguration<OrganizationStructure>
    {
        public void Configure(EntityTypeBuilder<OrganizationStructure> builder)
        {
            builder
                .HasOne(p => p.Organization)
                .WithMany(p => p.OrganizationStructures)
                .HasForeignKey(p => p.OrganizationId);

            builder
                .Property(p => p.NodeLabel).HasMaxLength(LengthConsts.MaxStringLen200);
        }
    }
}
