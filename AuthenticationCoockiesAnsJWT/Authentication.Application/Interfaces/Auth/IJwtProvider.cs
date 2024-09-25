using Authentication.Core.Models;

namespace Authentication.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
