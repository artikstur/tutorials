using Authorization.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Infrastructure
{
    public class PermissionRequirement(Permissions[] permissions) : IAuthorizationRequirement
    {
        public Permissions[] Permissions { get; set; } = permissions;
    }
}
