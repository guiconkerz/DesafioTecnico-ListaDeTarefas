using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface ICriarUsuarioHandler
    {
        Task<IResponse> Handle(CriarUsuarioRequest request);
    }
}
