using Authorization.Core.Enums;
using Authorization.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermissionEntity>(
                    l => l.HasOne<PermissionEntity>().WithMany().HasForeignKey(e => e.PermissionId),
                    r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RoleId));
            
            //Получаем все роли в системе
            var roles = Enum
                .GetValues<Role>()
                .Select(r => new RoleEntity
                {
                    Id = (int)r,
                    Name = r.ToString()
                });

            builder.HasData(roles);
            ////15 16
            //builder
            //    .HasMany(r => r.Users)
            //    .WithMany(u => u.Roles);
        }
    }
}
