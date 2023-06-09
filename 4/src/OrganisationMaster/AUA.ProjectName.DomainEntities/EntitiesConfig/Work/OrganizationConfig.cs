using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.DomainEntities.Entities.Work;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUA.ProjectName.DomainEntities.EntitiesConfig.Work
{
    public class OrganizationConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Organizations)
                .HasForeignKey(p => p.AppUserId);

            builder
                .Property(p => p.Name).HasMaxLength(LengthConsts.MaxStringLen200);
        }
    }
}
