using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Infra.Queries
{
    public class TarefasQueries : ITarefasQueries
    {
        private readonly TarefasDbContext _tarefasContext;

        public TarefasQueries(TarefasDbContext tarefasContext)
        {
            _tarefasContext = tarefasContext;
        }

        public async Task<Tarefa> ListarTarefa(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .Where(x => x.TarefaId == idTarefa)
                  .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaFinalizada(int idTarefa) =>
            await _tarefasContext
            .Tarefas
            .Where(x => x.TarefaId == idTarefa && x.Finalizada)
            .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaEmAndamento(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .Where(x => x.TarefaId == idTarefa && x.Finalizada == false)
                  .FirstOrDefaultAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodas() =>
             await _tarefasContext
                   .Tarefas
                   .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasFinalizadas() =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.Finalizada == true)
                  .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasEmAndamento() =>
             await _tarefasContext
                   .Tarefas
                   .AsNoTracking()
                   .Where(x => x.Finalizada == false)
                   .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasDoUsuario(int idUsuario) =>
             await _tarefasContext
                   .Tarefas
                   .AsNoTracking()
                   .Where(x => x.FkUsuario == idUsuario)
                   .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasFinalizadas(int idUsuario) =>
            await _tarefasContext
                  .Tarefas
                  .Where(x => x.FkUsuario == idUsuario && x.Finalizada == true)
                  .ToListAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodasEmAndamento(int idUsuario) =>
            await _tarefasContext
                  .Tarefas
                  .Where(x => x.FkUsuario == idUsuario && x.Finalizada == false)
                  .ToListAsync();
    }
}
