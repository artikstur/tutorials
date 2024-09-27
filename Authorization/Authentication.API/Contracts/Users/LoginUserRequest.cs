using System.ComponentModel.DataAnnotations;

namespace Authorization.API.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
