using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Interfaces.Tarefas
{
    public interface ITarefasRepositorio
    {
        Task AdicionarAsync(Tarefa tarefa);
        Task<bool> AtualizarAsync(Tarefa tarefa);
        Task<bool> RemoverAsync(int id);
        Task<bool> AlterarStatus(Tarefa tarefa);
        Task<Tarefa> ObterPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ListarTodas();
        Task<bool> AlterarDescricao(Tarefa tarefa);
        Task<bool> AlterarTitulo(Tarefa tarefa);
    }
}
