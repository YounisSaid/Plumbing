using CoreLayer.BaseEntities;
using System.Linq.Expressions;

namespace RepositoryLayer.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetAllCountAsync();
    }
}
