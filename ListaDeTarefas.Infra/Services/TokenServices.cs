using ListaDeTarefas.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListaDeTarefas.Infra.Services
{
    public sealed class TokenServices
    {
        public string Criar(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(PrivateKey.Key);
            var credentials = new SigningCredentials(key: new SymmetricSecurityKey(key),
                                                    algorithm: SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = GerarClaim(usuario)
            };

            

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private ClaimsIdentity GerarClaim(Usuario usuario)
        {
            var claimIdentity = new ClaimsIdentity();

            claimIdentity.AddClaim(new Claim(type: ClaimTypes.Email, value: usuario.Email.Endereco));
            claimIdentity.AddClaim(new Claim(type: ClaimTypes.GivenName, value: usuario.Login.Username));

            return claimIdentity;
        }
    }
}
