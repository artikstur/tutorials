using Authorization.Core.Models;

namespace Authorization.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
