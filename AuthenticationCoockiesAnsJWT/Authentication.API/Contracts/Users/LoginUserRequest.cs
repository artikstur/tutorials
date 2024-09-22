using System.ComponentModel.DataAnnotations;

namespace Authentication.API.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
