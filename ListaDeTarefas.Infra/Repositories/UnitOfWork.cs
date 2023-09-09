using ListaDeTarefas.Application.Interfaces.UnitOfWork;
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace ListaDeTarefas.Infra.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly TarefasDbContext _tarefasContext;

        public UnitOfWork(TarefasDbContext tarefasContext)
        {
            _tarefasContext = tarefasContext;
        }

        public void BeginTransaction()
        {
            _transaction = _tarefasContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _tarefasContext.SaveChanges();
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }

        
    }
}
