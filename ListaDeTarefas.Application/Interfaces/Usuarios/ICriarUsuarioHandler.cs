using ListaDeTarefas.Application.Usuarios.Commands.Criar.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface ICriarUsuarioHandler
    {
        Task<IResponse> Handle(CriarUsuarioRequest request);
    }
}
