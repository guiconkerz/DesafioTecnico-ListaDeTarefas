using ListaDeTarefas.Application.DTO;
using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Infra.Queries
{
    public interface ITarefasQueries
    {
        Task<Tarefa> ListarTarefa(int idTarefa);
        Task<Tarefa> ListarTarefaEmAndamento(int idTarefa);
        Task<Tarefa> ListarTarefaFinalizada(int idTarefa);
        Task<IEnumerable<TarefaDTO>> ListarTodas();
        Task<IEnumerable<TarefaDTO>> ListarTodasFinalizadas();
        Task<IEnumerable<TarefaDTO>> ListarTodasEmAndamento();
        Task<IEnumerable<TarefaDTO>> ListarTodasDoUsuario(int idUsuario);
        Task<IEnumerable<TarefaDTO>> ListarTodasFinalizadas(int idUsuario);
        Task<IEnumerable<TarefaDTO>> ListarTodasEmAndamento(int idUsuario);
    }
}