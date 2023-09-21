using ListaDeTarefas.Application.Interfaces.Usuarios.Handler;
using ListaDeTarefas.Application.Usuarios.Commands.AlterarSenha.Request;
using ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Application.Usuarios.Commands.SolicitarAlteracaoSenha.Request;
using ListaDeTarefas.Infra.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPut]
        [Route("/SolicitarCódigoAlteracaoSenha")]
        public async Task<IActionResult> SolicitarCódigoAlteracaoSenha(
            [FromBody] SolicitarAlteracaoSenhaRequest request,
            [FromServices] ISolicitarAlteracaoSenhaHandler _handler)
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

        [Authorize]
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

        [Authorize]
        [HttpPut]
        [Route("/AtivarConta")]
        public async Task<IActionResult> AtivarConta(
            [FromBody] AtivarContaRequest request,
            [FromServices] IAtivarContaHandler _handler)
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

        [Authorize]
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

       

    }
}
