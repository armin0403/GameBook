using System.Linq.Expressions;

namespace GameBook.Infrastructure.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindByIdAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync (TEntity entity);
        Task UpdateAsync (TEntity entity);
        void Delete (TEntity entity);
    }
}
