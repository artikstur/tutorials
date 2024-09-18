using Microsoft.EntityFrameworkCore;
using PersistencePostgres;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<LearningDbContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
