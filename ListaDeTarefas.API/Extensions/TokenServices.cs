using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ListaDeTarefas.API.Extensions
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration __configuration;
        private static List<(string, string)> __refreshTokens = new();

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

        public string GerarToken(IEnumerable<Claim> claims)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(__configuration["Secrets:JwtPrivateKey"]);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = credentials,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GerarClaims(LogarResponse response)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("Id", response.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, response.Login));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, response.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, response.Perfil));

            return claimsIdentity;
        }
        public string GerarRefreshToken()
        {
            //return Guid.NewGuid().ToString();
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal ObterClaimPrincipalDeTokenExpirado(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(__configuration["Secrets:JwtPrivateKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(value: SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(message: "Token inválido!");
            }
            return principal;
        }

        public void SalvarRefreshToken(string email, string refreshToken)
        {
            __refreshTokens.Add(new(email, refreshToken));
        }

        public string ObterRefreshToken(string email)
        {
            return __refreshTokens.FirstOrDefault(x => x.Item1 == email).Item2;
        }

        public void ExcluirRefreshToken(string email, string refreshToken)
        {
            var item = __refreshTokens.FirstOrDefault(x => x.Item1 == email && x.Item2 == refreshToken);
            __refreshTokens.Remove(item);
        }
    }
}
