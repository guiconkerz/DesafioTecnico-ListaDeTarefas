using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using System.Security.Claims;

namespace ListaDeTarefas.Application.Interfaces.Services
{
    public interface ITokenServices
    {
        string GerarToken(LogarResponse response);
        string GerarToken(IEnumerable<Claim> claims);
        string GerarRefreshToken();
        ClaimsPrincipal ObterClaimPrincipalDeTokenExpirado(string token);
        void SalvarRefreshToken(string email, string refreshToken);
        string ObterRefreshToken(string email);
        void ExcluirRefreshToken(string email, string refreshToken);
    }
}
