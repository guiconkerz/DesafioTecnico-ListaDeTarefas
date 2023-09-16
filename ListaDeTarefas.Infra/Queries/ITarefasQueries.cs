using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Tarefas.Queries
{
    public interface ITarefasQueries
    {
        Task<Tarefa> ListarTarefa(int id);
        Task<Tarefa> ListarTarefaEmAndamento(int id);
        Task<Tarefa> ListarTarefaFinalizada(int id);
        Task<IEnumerable<Tarefa>> ListarTodas();
        Task<IEnumerable<Tarefa>> ListarTodasFinalizadas();
        Task<IEnumerable<Tarefa>> ListarTodasEmAndamento();
    }
}