using CoreLayer.BaseEntities;
using RepositoryLayer.Repositories.Abstract;

namespace RepositoryLayer.UnitOfWorks.Abstract
{
    public interface IUnitOfWork
    {
        void Commit();
        Task<bool> CommitAsync();
        ValueTask DisposeAsync();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity, new();
    }
}
