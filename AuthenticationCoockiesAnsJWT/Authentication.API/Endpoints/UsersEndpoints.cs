using Authentication.API.Contracts.Users;
using Authentication.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            app.MapGet("users", GetUsers)
                .RequireAuthorization();

            return app;
        }

        public static async Task<IResult> Register([FromBody] RegisterUserRequest request, UsersService usersService)
        {
            await usersService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        public static async Task<IResult> Login([FromBody] LoginUserRequest request, UsersService usersService, HttpContext context)
        {
            // проверяем email и пароль
            // создаем токен, который подтверждает, что мы зашли в систему
            // сохранить токен в куки

            var token = await usersService.Login(request.Email, request.Password);
            context.Response.Cookies.Append("tasty-cookies", token);
            return Results.Ok(token);
        }

        public static async Task<IResult> GetUsers(UsersService usersService)
        {
            var users = await usersService.GetUsers();
            return Results.Ok(users);
        }
    }
}
