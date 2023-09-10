using ListaDeTarefas.Application.Usuarios.Commands.Excluir.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface IExcluirUsuarioHandler
    {
        Task<IResponse> Handle(ExcluirUsuarioRequest request);
    }
}
