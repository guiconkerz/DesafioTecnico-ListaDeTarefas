using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Application.Tarefas.Queries
{
    public class TarefasQueries : ITarefasQueries
    {
        private readonly TarefasDbContext _tarefasContext;

        public TarefasQueries(TarefasDbContext tarefasContext)
        {
            _tarefasContext = tarefasContext;
        }

        public async Task<Tarefa> ListarTarefa(int id) =>
            await _tarefasContext
            .Tarefas
            .Where(x => x.TarefaId == id)
            .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaFinalizada(int id) =>
            await _tarefasContext
            .Tarefas
            .Where(x => x.TarefaId == id && x.Finalizada)
            .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaEmAndamento(int id) =>
            await _tarefasContext
            .Tarefas
            .Where(x => x.TarefaId == id && x.Finalizada == false)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodas() =>
             await _tarefasContext
            .Tarefas
            .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasFinalizadas() =>
            await _tarefasContext
           .Tarefas
           .Where(x => x.Finalizada)
           .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasEmAndamento() =>
            await _tarefasContext
           .Tarefas
           .Where(x => x.Finalizada)
           .ToListAsync();
    }
}
