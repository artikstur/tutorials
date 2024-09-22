using Authentication.Application.Interfaces.Auth;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Core.Models;

namespace Authentication.Application.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, email, hashedPassword);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            // 10.16
            return "";
        }
    }
}
