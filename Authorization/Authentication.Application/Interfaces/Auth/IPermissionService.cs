using Authorization.Core.Enums;

namespace Authorization.Application.Interfaces.Auth
{
    public interface IPermissionService
    {
        Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId);
    }
}
