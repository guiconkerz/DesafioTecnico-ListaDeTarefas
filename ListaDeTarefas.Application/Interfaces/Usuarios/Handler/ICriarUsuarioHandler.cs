using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface ICriarUsuarioHandler
    {
        Task<IResponse> Handle(CriarUsuarioRequest request);
    }
}
