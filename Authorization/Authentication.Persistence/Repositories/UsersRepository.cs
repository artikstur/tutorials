using Authorization.Application.Interfaces.Repositories;
using Authorization.Core.Enums;
using Authorization.Core.Models;
using Authorization.Persistence.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.ConstrainedExecution;

namespace Authorization.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LearningDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(LearningDbContext dbContext, IMapper mapper, ILogger<UsersRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Add(User user)
        {
            var roleEntity = await _dbContext.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin);

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Roles = new List<RoleEntity> { roleEntity },
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

        public async Task DeleteAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            _dbContext.Users.RemoveRange(users);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<HashSet<Permissions>> GetUserPermissions(Guid userId)
        {
            _logger.LogInformation($"смотрим разрешения");
            var roles = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            var res = roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permissions)p.Id)
                .ToHashSet();

            foreach (var per in res)
            {
                _logger.LogInformation($"разрешения для данного пользователя: {per.ToString()}");
            }

            return res;
        }
    }
}
