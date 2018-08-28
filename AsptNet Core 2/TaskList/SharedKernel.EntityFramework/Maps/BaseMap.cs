using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Modelo;

namespace SharedKernel.EntityFramework.Maps
{
    public class BaseMap
    {
        protected void ConfigureMap<T>(EntityTypeBuilder<T> map, string primaryKey) where T : EntityBase
        {
            map.HasKey(x => x.Id)
               .HasName(primaryKey);
            map.Property(x => x.Id)
               .UseSqlServerIdentityColumn();

            map.Property(x => x.createDateTime).HasColumnName("createDateTime");
            map.Property(x => x.createUserName).HasMaxLength(50).HasColumnName("createUserName");
            map.Property(x => x.updateDateTime).HasColumnName("updateDateTime");
            map.Property(x => x.updateUserName).HasMaxLength(50).HasColumnName("updateUserName");
            map.Property(x => x.deleteDateTime).HasColumnName("deleteDateTime");
            map.Property(x => x.deleteUserName).HasMaxLength(50).HasColumnName("deleteUserName");
        }
    }
}
