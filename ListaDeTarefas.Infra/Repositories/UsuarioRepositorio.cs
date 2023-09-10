using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ListaDeTarefas.Infra.Repositories
{
    public sealed class UsuarioRepositorio : IUsuarioRepositorio
    {
        public readonly TarefasDbContext _tarefasContext;
        public readonly IRepositorioBase<Usuario> _repositorioBase;

        public UsuarioRepositorio(TarefasDbContext tarefasContext, IRepositorioBase<Usuario> repositorioBase)
        {
            _tarefasContext = tarefasContext;
            _repositorioBase = repositorioBase;
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            try
            {
                await _repositorioBase.AdicionarAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Usuario> BuscarPorIdAsync(int id) => await _repositorioBase.ObterPorId(x => x.UsuarioId == id);

        public async Task<bool> RemoverAsync(int id)
        {
            var removido = await _tarefasContext
                .Usuarios
                .Where(x => x.UsuarioId == id)
                .ExecuteDeleteAsync();

            if (removido == 0)
            {
                return false;
            }
            return true;
        }
    }
}
