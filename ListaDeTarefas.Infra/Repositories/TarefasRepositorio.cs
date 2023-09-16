using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Tarefas;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Infra.Repositories
{
    public sealed class TarefasRepositorio : ITarefasRepositorio
    {
        public readonly TarefasDbContext _tarefasContext;
        public readonly IRepositorioBase<Tarefa> _repositorioBase;

        public TarefasRepositorio(TarefasDbContext tarefasContext, IRepositorioBase<Tarefa> repositorioBase)
        {
            _tarefasContext = tarefasContext;
            _repositorioBase = repositorioBase;
        }

        public async Task AdicionarAsync(Tarefa tarefa) => _tarefasContext.Tarefas.Add(tarefa);

        public async Task<bool> AtualizarAsync(Tarefa tarefa)
        {
            var atualizado = await _tarefasContext
                .Tarefas
                .Where(x => x.TarefaId == tarefa.TarefaId)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(x => x.Titulo, tarefa.Titulo)
                     .SetProperty(x => x.Descricao, tarefa.Descricao)
                     .SetProperty(x => x.DataEntrega, tarefa.DataEntrega)
                     .SetProperty(x => x.Finalizada, tarefa.Finalizada));

            if (atualizado == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AlterarStatus(Tarefa tarefa)
        {
            var atualizado = await _tarefasContext
                .Tarefas
                .Where(x => x.TarefaId == tarefa.TarefaId)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(x => x.Finalizada, tarefa.Finalizada));

            if (atualizado == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> RemoverAsync(int id)
        {
            var removido = await _tarefasContext
                .Tarefas
                .Where(x => x.TarefaId == id)
                .ExecuteDeleteAsync();

            if (removido == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Tarefa> ObterPorIdAsync(int id) =>
            await _tarefasContext
                    .Tarefas
                    .AsNoTracking()
                    .Where(x => x.TarefaId == id)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<Tarefa>> ListarTodas() =>
            await _tarefasContext
            .Tarefas
            .AsNoTracking()
            .OrderBy(x => x.TarefaId)
            .ToListAsync();
    }
}
