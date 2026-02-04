using RepositoryLayer.Context;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
using RepositoryLayer.UnitOfWorks.Abstract;

namespace RepositoryLayer.UnitOfWorks.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        IGenericRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            return new GenericRepository<TEntity>(_context);
        }

    }
}
