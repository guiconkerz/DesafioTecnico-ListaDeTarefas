using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Infra.Queries
{
    public interface ITarefasQueries
    {
        Task<Tarefa> ListarTarefa(int idTarefa);
        Task<Tarefa> ListarTarefaEmAndamento(int idTarefa);
        Task<Tarefa> ListarTarefaFinalizada(int idTarefa);
        Task<IEnumerable<Tarefa>> ListarTodas();
        Task<IEnumerable<Tarefa>> ListarTodasFinalizadas();
        Task<IEnumerable<Tarefa>> ListarTodasEmAndamento();
        Task<IEnumerable<Tarefa>> ListarTodasDoUsuario(int idUsuario);
        Task<IEnumerable<Tarefa>> ListarTodasFinalizadas(int idUsuario);
        Task<IEnumerable<Tarefa>> ListarTodasEmAndamento(int idUsuario);
    }
}