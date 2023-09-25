using ListaDeTarefas.Application.Tarefas.Commands.Excluir.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface IExcluirTarefaHandler
    {
        Task<IResponse> Handle(ExcluirTarefaRequest request);
    }
}
