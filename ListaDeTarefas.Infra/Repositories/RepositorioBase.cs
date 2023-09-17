using ListaDeTarefas.Application.Interfaces.RepositoryBase;
using ListaDeTarefas.Infra.Data.Context;
using ListaDeTarefas.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ListaDeTarefas.Infra.Repositories
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : Entity
    {
        private readonly TarefasDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositorioBase(TarefasDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AdicionarAsync(T entity) => await _dbSet.AddAsync(entity);

        public void RemoverAsync(T entity) => _dbSet.Remove(entity);

        public void Atualizar(T entity) => _dbSet.Update(entity);

        public async Task<T> ObterPorId(Expression<Func<T, bool>> predicate)
        {
            if (predicate is not null)
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }
            return await _dbSet.FirstOrDefaultAsync();
        }

        
    }
}
