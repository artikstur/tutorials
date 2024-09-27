using Authorization.Application.Interfaces.Auth;
using Authorization.Application.Interfaces.Repositories;
using Authorization.Core.Enums;

namespace Authorization.Application.Services
{
    public class PermissionService: IPermissionService
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId)
        {
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}
