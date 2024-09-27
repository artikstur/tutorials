using Authorization.Core.Enums;
using Authorization.Core.Models;

namespace Authorization.Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
        Task DeleteAllUsers();
    }
}
