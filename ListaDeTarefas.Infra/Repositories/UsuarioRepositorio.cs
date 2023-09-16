using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Application.Interfaces.Usuarios;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task AdicionarAsync(Usuario usuario) => 
            await _repositorioBase
            .AdicionarAsync(usuario);

        public async Task<bool> AlterarEmail(Usuario usuario)
        {
            var alterado =
                _tarefasContext
               .Usuarios
               .Where(x => x.UsuarioId == usuario.UsuarioId)
               .ExecuteUpdate(x =>
                x.SetProperty(x => x.Email.Endereco, usuario.Email.Endereco));

            if (alterado == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AlterarSenha(Usuario usuario)
        {
            var alterado = _tarefasContext
                            .Usuarios
                            .Where(x => x.UsuarioId == usuario.UsuarioId)
                            .ExecuteUpdate(x =>
                                x.SetProperty(x => x.Senha.Password, usuario.Senha.Password));

            if (alterado == 0)
            {
                return false;
            }
            return true;
        }

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

        public async Task<Usuario> BuscarPorIdAsync(int id) =>
            await _tarefasContext
            .Usuarios
            .AsNoTracking()
            .Where(x => x.UsuarioId == id)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Usuario>> ListarTodos() =>
            await _tarefasContext
            .Usuarios
            .AsNoTracking()
            .OrderBy(x => x.UsuarioId)
            .ToListAsync();
    }
}
