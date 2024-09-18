using Microsoft.EntityFrameworkCore;
using SecontTest.Models;

namespace SecontTest.DataAccess;

public class NotesDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public NotesDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //коллекция сущностей
    public DbSet<Note> Notes => Set<Note>(); 

    // будет вызываться, когда мы будем использовать дб контекст
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));

    }
}