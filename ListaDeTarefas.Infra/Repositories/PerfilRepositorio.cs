using ListaDeTarefas.Application.Interfaces.Perfis;
using ListaDeTarefas.Domain.Models;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Infra.Repositories
{
    public sealed class PerfilRepositorio : IPerfilRepositorio
    {
        private readonly TarefasDbContext _context;

        public PerfilRepositorio(TarefasDbContext context)
        {
            _context = context;
        }

        public async Task<Perfil> ObterPorIdAsync(int id) =>
            await _context
                 .Perfis
                 .Where(x => x.PerfilId == id)
                 .FirstOrDefaultAsync();

        public async Task<Perfil> ObterPorNomeAsync(string nome) =>
            await _context
            .Perfis
            .Where(x => x.Nome.Contains(nome))
            .FirstOrDefaultAsync();
    }
}
