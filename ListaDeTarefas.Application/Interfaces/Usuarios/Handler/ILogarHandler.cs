using ListaDeTarefas.Application.Usuarios.Commands.Login.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Usuarios.Handler
{
    public interface ILogarHandler
    {
        Task<IResponse> Handle(LogarRequest request);
    }
}
