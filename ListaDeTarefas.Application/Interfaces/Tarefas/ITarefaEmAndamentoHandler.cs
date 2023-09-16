using ListaDeTarefas.Application.Tarefas.Commands.MarcarEmAndamento.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ITarefaEmAndamentoHandler
    {
        Task<IResponse> Handle(TarefaEmAndamentoRequest request);
    }
}
