using ListaDeTarefas.Domain.Models;

namespace ListaDeTarefas.Application.Interfaces.Perfis
{
    public interface IPerfilRepositorio
    {
        Task<Perfil> ObterPorIdAsync(int id);
        Task<Perfil> ObterPorNomeAsync(string nome);
    }
}
