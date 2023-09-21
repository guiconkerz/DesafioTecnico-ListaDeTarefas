using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ITarefaEmAndamentoHandler
    {
        Task<IResponse> Handle(TarefaEmAndamentoRequest request);
    }
}
