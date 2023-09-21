using ListaDeTarefas.Application.DTO;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ListaDeTarefas.Infra.Queries
{
    public class TarefasQueries : ITarefasQueries
    {
        private readonly TarefasDbContext _tarefasContext;

        public TarefasQueries(TarefasDbContext tarefasContext)
        {
            _tarefasContext = tarefasContext;
        }

        public async Task<Tarefa> ListarTarefaFinalizada(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa && x.Finalizada == true)
                  .Include(x => x.Usuario)
                  .FirstOrDefaultAsync();

        public async Task<Tarefa> ListarTarefaEmAndamento(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa && x.Finalizada == false)
                  .Include(x => x.Usuario)
                  .FirstOrDefaultAsync();

        public async Task<TarefaDTO> ListarTarefa(int idTarefa) =>
            await _tarefasContext
                  .Tarefas
                  .AsNoTracking()
                  .Where(x => x.TarefaId == idTarefa)
                  .Select(t =>
                        new TarefaDTO(
                            t.TarefaId,
                            t.Titulo,
                            t.Descricao,
                            t.DataEntrega,
                            t.Finalizada,
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
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
                            t.Usuario.Login.Username))
                  .ToListAsync();
    }
}
