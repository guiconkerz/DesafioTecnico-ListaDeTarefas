using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Interfaces.Usuarios
{
    public interface IUsuarioRepositorio
    {
        Task AdicionarAsync(Usuario usuario);
        Task<bool> RemoverAsync(int id);
        Task<bool> AlterarSenha(Usuario usuario);   
        Task<bool> AlterarEmail(Usuario usuario);
        Task<Usuario> BuscarPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ListarTodos();
        Task<bool> EmailCadastrado(string email);
        Task<Usuario> BuscarPorEmailAsync(string email);
        Task<bool> AtivarConta(Usuario usuario);
        Task<bool> CadastrarCodigoAlteracaoSenha(Usuario usuario);
    }
}
