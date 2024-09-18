using Microsoft.EntityFrameworkCore;
using Persistence.Models;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }
}