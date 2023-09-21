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
        private readonly DateTime? __dataNula = null;
        private readonly string? __codNulo = null;

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
            DateTime? dataNula = null;
            var alterado = await _tarefasContext
                            .Usuarios
                            .Where(x => x.UsuarioId == usuario.UsuarioId)
                            .ExecuteUpdateAsync(x =>
                                x.SetProperty(x => x.Senha.Password, usuario.Senha.Password)
                                .SetProperty(x => x.Senha.DataExpiracao, dataNula)
                                .SetProperty(x => x.Senha.DataVerificacao, DateTime.Now)
                                .SetProperty(x => x.Senha.CodigoAlteracao, __codNulo));

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
            .Where(x => x.UsuarioId == id)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Usuario>> ListarTodos() =>
            await _tarefasContext
            .Usuarios
            .OrderBy(x => x.UsuarioId)
            .ToListAsync();

        public async Task<bool> EmailCadastrado(string email) =>
            await _tarefasContext
            .Usuarios
            .AsNoTracking()
            .AnyAsync(x => x.Email.Endereco == email);

        public async Task<Usuario> BuscarPorEmailAsync(string email) =>
            await _tarefasContext
                  .Usuarios
                  .Include(x => x.Perfil)
                  .Where(x => x.Email.Endereco == email)
                  .FirstOrDefaultAsync();

        public async Task<bool> AtivarConta(Usuario usuario)
        {
            
            var ativado = await _tarefasContext
                  .Usuarios
                  .Where(x => x.Email.Endereco == usuario.Email.Endereco && x.Email.VerificarEmail.Codigo == usuario.Email.VerificarEmail.Codigo)
                  .ExecuteUpdateAsync(x =>
                    x.SetProperty(x => x.Email.VerificarEmail.DataExpiracao, __dataNula)
                     .SetProperty(x => x.Email.VerificarEmail.DataVerificacao, DateTime.Now)
                     .SetProperty(x => x.Email.VerificarEmail.Codigo, __codNulo));

            if (ativado == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CadastrarCodigoAlteracaoSenha(Usuario usuario)
        {
            var alterado = await _tarefasContext
                                .Usuarios
                                .Where(x => x.UsuarioId == usuario.UsuarioId)
                                .ExecuteUpdateAsync(x => x.SetProperty(x => x.Senha.CodigoAlteracao, usuario.Senha.CodigoAlteracao));

            if (alterado == 0)
            {
                return false;
            }
            return true;
        }
            
    }
}
