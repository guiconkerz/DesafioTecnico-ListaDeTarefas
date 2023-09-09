using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeTarefas.Application.Interfaces.RepositoryBase
{
    public interface IRepositorioBase<T> where T : class
    {
        Task AdicionarAsync(T entity);
        void RemoverAsync(T entity);
        void Atualizar(T entity);
        Task<T> ObterPorId(Expression<Func<T, bool>> predicate);
    }
}
