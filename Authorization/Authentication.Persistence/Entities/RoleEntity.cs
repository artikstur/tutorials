using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.Entities
{
    // Конкретно роль админа там например
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Разрешения на каждый эндпоинт
        public ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
