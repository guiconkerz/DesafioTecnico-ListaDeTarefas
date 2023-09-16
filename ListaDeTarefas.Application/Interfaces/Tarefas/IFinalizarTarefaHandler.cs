using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface IFinalizarTarefaHandler
    {
        Task<IResponse> Handle(FinalizarTarefaRequest request);
    }
}
