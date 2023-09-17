using ListaDeTarefas.Application.DTO;
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
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa)
                  .Select(x =>
                        new Tarefa(x.Titulo,
                                   x.Descricao,
                                   x.DataEntrega,
                                   x.Finalizada))
                  .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaFinalizada(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa && x.Finalizada)
                  .Select(x =>
                        new Tarefa(x.Titulo,
                                   x.Descricao,
                                   x.DataEntrega,
                                   x.Finalizada))
                  .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaEmAndamento(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa && x.Finalizada == false)
                  .Select(x => 
                        new Tarefa(x.Titulo,
                                   x.Descricao,
                                   x.DataEntrega,
                                   x.Finalizada))
                  .FirstOrDefaultAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodas() =>
             await _tarefasContext
                   .Tarefas
                   .AsNoTracking()
                   .Select(t => 
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                   .ToListAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodasFinalizadas() =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.Finalizada == true)
                   .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                  .ToListAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodasEmAndamento() =>
             await _tarefasContext
                   .Tarefas
                   .AsNoTracking()
                   .Where(x => x.Finalizada == false)
                   .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                   .ToListAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodasDoUsuario(int idUsuario) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(t => t.Usuario.UsuarioId == idUsuario)
                  .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                  .ToListAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodasFinalizadas(int idUsuario) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.Usuario.UsuarioId == idUsuario && x.Finalizada == true)
                  .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                  .ToListAsync();

        public async Task<IEnumerable<TarefaDTO>> ListarTodasEmAndamento(int idUsuario) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.Usuario.UsuarioId == idUsuario && x.Finalizada == false)
                  .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.FkUsuario))
                  .ToListAsync();
    }
}
