using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Application.Interfaces.Auth;
using Authorization.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Infrastructure
{
    public class JwtProvider: IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generate(User user)
        {
            // Claims — это информация, которую ты помещаешь в токен.
            // Например, можно указать идентификатор пользователя, роль и
            // другую информацию, которая будет закодирована в токене
            // и сможет быть расшифрована с помощью секретного ключа.

            Claim[] claims =
            {
                new Claim(CustomClaims.UserId, user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler;
        }
    }
}
