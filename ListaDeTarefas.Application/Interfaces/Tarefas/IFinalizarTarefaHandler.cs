using ListaDeTarefas.Application.Tarefas.Commands.MarcarFinalizada.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface IFinalizarTarefaHandler
    {
        Task<IResponse> Handle(FinalizarTarefaRequest request);
    }
}
