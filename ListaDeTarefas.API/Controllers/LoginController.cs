using ListaDeTarefas.Application.Interfaces.Services;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Login.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        [Route("/CriarUsuario")]
        public async Task<IActionResult> Criar(
            [FromServices] ICriarUsuarioHandler _handler,
            [FromBody] CriarUsuarioRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("/Logar")]
        public async Task<IActionResult> Logar(
            [FromBody] LogarRequest request,
            [FromServices] ILogarHandler _handler)
        {
            var response = await _handler.Handle(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("/RefreshToken")]
        public async Task<IActionResult> RefreshToken(
            [FromServices] ITokenServices _tokenServices,
            [FromBody] RefreshTokenRequest request)
        {
            var principal = _tokenServices.ObterClaimPrincipalDeTokenExpirado(request.Token);
            var usuario = principal.Identity.Name;
            var refreshTokenSalvo = _tokenServices.ObterRefreshToken(usuario);
            if (refreshTokenSalvo is null || refreshTokenSalvo != request.RefreshToken)
            {
                throw new SecurityTokenException(message: "Refresh Token inválido!");
            }

            var novoToken = _tokenServices.GerarToken(principal.Claims);
            var novoRefreshToken = _tokenServices.GerarRefreshToken();
            _tokenServices.ExcluirRefreshToken(usuario, request.RefreshToken);
            _tokenServices.SalvarRefreshToken(usuario, novoRefreshToken);

            var response = new RefreshTokenResponse(token: novoToken,
                                                    refreshToken: novoRefreshToken,
                                                    data: DateTime.Now);
            return Ok(response);
        }
    }
}
