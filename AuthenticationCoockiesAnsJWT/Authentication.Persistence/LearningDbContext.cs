using Authentication.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence
{
    public class LearningDbContext: DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
