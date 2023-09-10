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

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            try
            {
                await _repositorioBase.AdicionarAsync(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> AtualizarAsync(Tarefa tarefa)
        {
            var atualizado = await _tarefasContext
                .Tarefas
                .Where(x => x.Id == tarefa.Id)
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
    }
}
