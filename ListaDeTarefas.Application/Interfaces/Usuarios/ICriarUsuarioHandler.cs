using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Application.Usuarios.Commands.Criar.Response;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface ICriarUsuarioHandler
    {
        Task<CriarUsuarioResponse> Handle(CriarUsuarioRequest request);
    }
}
