using Authentication.Application.Interfaces.Repositories;
using Authentication.Persistence;
using Authentication.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LearningDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
});

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

