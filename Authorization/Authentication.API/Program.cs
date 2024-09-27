using Authorization.API.Extensions;
using Authorization.Application.Interfaces.Auth;
using Authorization.Application.Interfaces.Repositories;
using Authorization.Application.Services;
using Authorization.Core.Enums;
using Authorization.Infrastructure;
using Authorization.Persistence;
using Authorization.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using AuthorizationOptions = Authorization.Persistence.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();


builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.AddDbContext<LearningDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(LearningDbContext)));
});

builder.Services.AddApiAuthentication(configuration);
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<UsersService>();

builder.Services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();

app.UseAuthorization();

app.AddMappedEndpoints();

app.MapGet("get", () =>
{
    return Results.Ok("OK!");
}).RequirePermissions(Permissions.Read);

app.MapPost("post", () =>
{
    return Results.Ok("OK!");
}).RequirePermissions(Permissions.Create);

app.Run();