using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Request;
using ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Request;
using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Application.Tarefas.Commands.Excluir.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request;
using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Infra.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ListaDeTarefas.API.Controllers
{
    [Authorize("Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> ListarTodas(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodas();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasFinalizadas")]
        public async Task<IActionResult> ListarTodasFinalizadas(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodasFinalizadas();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/TarefasEmAndamento")]
        public async Task<IActionResult> ListarTodasEmAndamento(
            [FromServices] ITarefasQueries queries)
        {
            var tarefas = await queries.ListarTodasEmAndamento();
            if (tarefas is null)
            {
                return NoContent();
            }
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("/ListarTarefa/{idTarefa}")]
        public async Task<IActionResult> Listar(
            [FromServices] ITarefasQueries queries,
            [FromRoute] int idTarefa)
        {
            var tarefa = await queries.ListarTarefa(idTarefa);
            if (tarefa is null)
            {
                return BadRequest($"Tarefa não encontrada.");
            }
            return Ok(tarefa);
        }

        [HttpPost]
        [Route("/CriarTarefa")]
        public async Task<IActionResult> Criar(
            [FromServices] ICriarTarefaHandler _handler,
            [FromBody] CriarTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/AlterarTitulo")]
        public async Task<IActionResult> AlterarTitulo(
            [FromServices] IAlterarTituloTarefaHandler _handler,
            [FromBody] AlterarTituloTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/AlterarDescricao")]
        public async Task<IActionResult> AlterarDescricao(
            [FromServices] IAlterarDescricaoTarefaHandler _handler,
            [FromBody] AlterarDescricaoTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/TarefaEmAndamento")]
        public async Task<IActionResult> AlterarStatus(
            [FromServices] ITarefaEmAndamentoHandler _handler,
            [FromBody] TarefaEmAndamentoRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("/Finalizar")]
        public async Task<IActionResult> FinalizarTarefa(
            [FromServices] IFinalizarTarefaHandler _handler,
            [FromBody] FinalizarTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("/Excluir")]
        public async Task<IActionResult> Excluir(
            [FromServices] IExcluirTarefaHandler _handler,
            [FromBody] ExcluirTarefaRequest request)
        {
            var response = await _handler.Handle(request);
            if (response.StatusCode is HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            if (response.StatusCode is HttpStatusCode.InternalServerError)
            {
                return StatusCode(500, response);
            }
            return Ok(response);
        }
    }
}
