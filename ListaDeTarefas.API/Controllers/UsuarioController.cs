using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("/")]
        public async Task<IResponse> Post(
            [FromServices] ICriarUsuarioHandler _handler,
            [FromBody] CriarUsuarioRequest request)
        {
            var response = await _handler.Handle(request);
            if (true)
            {

            }
            
            return response;
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IResponse> Delete(
            [FromServices] IExcluirUsuarioHandler _handler,
            [FromRoute] ExcluirUsuarioRequest request)
        {
            var response = await _handler.Handle(request);
            return response;
        }

        //[HttpGet]
        //[Route("/{id}")]
        //public async Task<BuscarUsuarioPorIdResponse> Get([FromRoute]BuscarUsuarioPorIdQuery request)
        //{
        //    var response = await _usuarioPorIdHandler.Handle(request);
        //    return response;
        //}

    }
}
