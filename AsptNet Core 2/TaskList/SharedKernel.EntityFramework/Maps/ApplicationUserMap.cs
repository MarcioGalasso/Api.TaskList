using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Entities;

namespace SharedKernel.EntityFramework.Maps
{
    public class ApplicationUserMap : BaseMap, IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> map)
        {
            ConfigureMap(map, "pk_applicationuser");

            map.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            map.HasIndex(x => x.Name).HasName("idx_applicationuser_name").IsUnique();
            map.Property(x => x.Secret).HasColumnName("Secret").HasMaxLength(100).IsRequired();
        }
    }
}
