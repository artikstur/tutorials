using Authentication.Core.Models;

namespace Authentication.Application.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}
