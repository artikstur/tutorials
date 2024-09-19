using Microsoft.EntityFrameworkCore;
using PersistencePostgres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistencePostgres.Repositories
{
    public class LessonsRepository
    {
        private readonly LearningDbContext _dbContext;
        public LessonsRepository(LearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLesson(Guid courseId, LessonEntity lessonEntity)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId)
                ?? throw new NullReferenceException();

            course.Lessons.Add(lessonEntity);
            
            await _dbContext.SaveChangesAsync();      
        }
    }
}
