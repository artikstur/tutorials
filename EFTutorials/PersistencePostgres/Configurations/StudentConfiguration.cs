using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistencePostgres.Models;

namespace PersistencePostgres.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students);
        }
    }
}
