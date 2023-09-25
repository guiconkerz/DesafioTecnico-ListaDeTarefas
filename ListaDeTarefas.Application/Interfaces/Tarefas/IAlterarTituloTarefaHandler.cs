using ListaDeTarefas.Application.Tarefas.Commands.AlterarTitulo.Request;
using ListaDeTarefas.Shared.Interfaces;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface IAlterarTituloTarefaHandler
    {
        Task<IResponse> Handle(AlterarTituloTarefaRequest request);
    }
}
