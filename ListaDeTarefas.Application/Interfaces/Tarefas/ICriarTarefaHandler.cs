using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Domain.Abstraction;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ICriarTarefaHandler
    {
        Task<IResponse> Handle(CriarTarefaRequest request);
    }
}
