using ListaDeTarefas.Application.Usuarios.Commands.AtivarConta.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface IAtivarContaHandler
    {
        Task<IResponse> Handle(AtivarContaRequest request);
    }
}
