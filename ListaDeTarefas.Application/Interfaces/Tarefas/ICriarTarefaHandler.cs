using ListaDeTarefas.Application.Tarefas.Commands.Criar.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ICriarTarefaHandler
    {
        Task<IResponse> Handle(CriarTarefaRequest request);
    }
}
