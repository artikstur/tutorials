using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersistencePostgres.Models;

namespace PersistencePostgres.Configurations
{
    internal class AuthorConfiguration: IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.Course)
                .WithOne(c => c.Author);
        }
    }
}
