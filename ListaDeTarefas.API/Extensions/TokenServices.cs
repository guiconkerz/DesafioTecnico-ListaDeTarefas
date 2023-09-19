using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListaDeTarefas.API.Extensions
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration __configuration;

        public TokenServices(IConfiguration configuration)
        {
            __configuration = configuration;
        }
        public string GerarToken(LogarResponse response)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(__configuration["Secrets:JwtPrivateKey"]);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GerarClaims(response),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = credentials,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GerarClaims(LogarResponse response)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, response.Login));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, response.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, response.Perfil));

            return claimsIdentity;
        }
    }
}
