using ListaDeTarefas.Application.Tarefas.Commands.AlterarDescricao.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface IAlterarDescricaoTarefaHandler
    {
        Task<IResponse> Handle(AlterarDescricaoTarefaRequest request);
    }
}
