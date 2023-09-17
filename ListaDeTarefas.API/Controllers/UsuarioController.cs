using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Queries;
using ListaDeTarefas.Infra.Services;
using ListaDeTarefas.Infra.Services.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio __usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            __usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [Route("/ListarTodas/{idUsuario}")]
        public async Task<IActionResult> ListarTodas(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int idUsuario)
        {
            var tarefas = await queries.ListarTodasDoUsuario(idUsuario);
            if (tarefas is null)
            {
                return BadRequest($"Usuario com id {idUsuario} não encontrado.");
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasFinalizadas/{idUsuario}")]
        public async Task<IActionResult> ListarTodasFinalizadas(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int idUsuario)
        {
            var tarefas = await queries.ListarTodasFinalizadas(idUsuario);
            if (tarefas is null)
            {
                return BadRequest($"Usuario com id {idUsuario} não encontrado.");
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasEmAndamento/{idUsuario}")]
        public async Task<IActionResult> ListarTodasEmAndamento(
            [FromServices] ITarefasQueries queries,
             [FromRoute] int idUsuario)
        {
            var tarefas = await queries.ListarTodasEmAndamento(idUsuario);
            if (tarefas is null)
            {
                return BadRequest($"Usuario com id {idUsuario} não encontrado.");
            }
            return Ok(tarefas);
        }

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

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> Excluir(
            [FromServices] IExcluirUsuarioHandler _handler,
            [FromRoute] int id)
        {
            var request = new ExcluirUsuarioRequest(id);
            var response = await _handler.Handle(request);

            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(
            [FromBody] AlterarSenhaRequest request,
            [FromServices] IAlterarSenhaHandler _handler)
        {
            var response = await _handler.Handle(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("/GerarToken/{idUsuario}")]
        public async Task<IActionResult> CriarToken(
            [FromServices] TokenServices services,
            [FromRoute] int idUsuario)
        {
            var usuario = await __usuarioRepositorio.BuscarPorIdAsync(idUsuario);
            if (usuario == null) return NotFound($"Usuário informado não encontrado.");
            var token = services.Criar(usuario);
            
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        [Route("/Restrito")]
        public async  Task<IActionResult> Restrito()
        {
            return Ok($"Bem vindo {User.Login()}, você está Autorizado!");
        }

        [Authorize("Admin")]
        [HttpGet]
        [Route("/Admin")]
        public async Task<IActionResult> Admin()
        {
            var claim = User.Identity.Name;
            return Ok($"Bem vindo {claim}");
        }

    }
}
