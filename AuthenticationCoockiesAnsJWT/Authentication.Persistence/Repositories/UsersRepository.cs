using Authentication.Application.Interfaces.Repositories;
using Authentication.Core.Models;
using Authentication.Persistence.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Repositories
{
    public class UsersRepository: IUsersRepository
    {
        private readonly LearningDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersRepository(LearningDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };

            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email)
                             ?? throw new Exception();

            return _mapper.Map<User>(userEntity);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userEntities = await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();

            return userEntities
                .Select(userEntity => _mapper.Map<User>(userEntity))
                .ToList();
        }
    }
}
