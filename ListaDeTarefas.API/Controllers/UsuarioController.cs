using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ListaDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ICriarUsuarioHandler _criarUsuarioHandler;
        private readonly IBuscarUsuarioPorIdHandler _usuarioPorIdHandler;

        public UsuarioController(IUnitOfWork unitOfWork, ICriarUsuarioHandler handler, IBuscarUsuarioPorIdHandler usuarioPorIdHandler)
        {
            _unitOfWork = unitOfWork;
            _criarUsuarioHandler = handler;
            _usuarioPorIdHandler = usuarioPorIdHandler;
        }

        [HttpPost]
        [Route("/")]
        public async Task<CriarUsuarioResponse> Post(
            [FromBody] CriarUsuarioRequest request)
        {
            var response = await _criarUsuarioHandler.Handle(request);
            
            return response;
        }

        //[HttpGet]
        //[Route("/{id}")]
        //public async Task<BuscarUsuarioPorIdResponse> Get([FromRoute]BuscarUsuarioPorIdQuery request)
        //{
        //    var response = await _usuarioPorIdHandler.Handle(request);
        //    return response;
        //}

        //// GET api/<UsuarioController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// PUT api/<UsuarioController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsuarioController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
