
using Microsoft.EntityFrameworkCore;
using PersistencePostgres.Models;

namespace PersistencePostgres.Repositories
{
    public class CoursesRepository
    {
        private readonly LearningDbContext _dbContext;
        public CoursesRepository(LearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseEntity>> Get()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        // когда мы получим курсы, то и уроки вытянем
        public async Task<List<CourseEntity>> GetWithLessons()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Lessons)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetById(Guid id)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<CourseEntity>> GetByFilter(string title, decimal price)
        {
            var query = _dbContext.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }

            if (price > 0)
            {
                query = query.Where(c => c.Price > price);
            }

            return await query.ToListAsync();
        }

        public async Task<List<CourseEntity>> GetByPage(string title, decimal price)
        {
            var query = _dbContext.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }

            if (price > 0)
            {
                query = query.Where(c => c.Price > price);
            }

            return await query.ToListAsync();
        }

        public async Task Add(Guid id, Guid authorId, string title, string description, decimal price)
        {
            var courseEntity = new CourseEntity
            {
                Id = id,
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price
            };

            await _dbContext.AddAsync(courseEntity);
            await _dbContext.SaveChangesAsync();
        }

        //обновление в одну операцию
        public async Task Update(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Courses
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Title, title)
                    .SetProperty(c => c.Description, description)
                    .SetProperty(c => c.Price, price));
        }


        public async Task Delete(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Courses
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}
