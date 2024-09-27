using Authorization.Core.Enums;
using Authorization.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.Configurations
{
    public class PermissionConfiguration: IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            var permissions = Enum
                .GetValues<Permissions>()
                .Select(p => new PermissionEntity
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
            //builder
            //    .HasMany(p => p.Roles)
            //    .WithMany(r => r.Permissions);
        }
    }
}
