using Authentication.API.Contracts.Users;
using Authentication.Application.Services;

namespace Authentication.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        public static async Task<IResult> Register(RegisterUserRequest request, UsersService usersService)
        {
            await usersService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        public static async Task<IResult> Login()
        {
            return Results.Ok();
        }
    }
}
