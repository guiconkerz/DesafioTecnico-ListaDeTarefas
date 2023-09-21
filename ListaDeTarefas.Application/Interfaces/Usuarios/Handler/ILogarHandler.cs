using ListaDeTarefas.Application.Usuarios.Commands.Login.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface ILogarHandler
    {
        Task<IResponse> Handle(LogarRequest request);
    }
}
