using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Repositories.Repositories.Abstractions
{
    public interface IBaseRespostories<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes the entity permanently.
        /// Should be used wisely, as it will delete the entity from the database.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes multiple entities permanently.
        /// Should be used wisely, as it will delete the entities from the database.
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetQueryable();
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList();
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
