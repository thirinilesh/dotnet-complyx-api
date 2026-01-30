using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Threading.Tasks;
using ComplyX.Repositories.Repositories.Abstractions;

namespace ComplyX.Repositories.Repositories
{

        public class BaseRespostories<TEntity, TKey> : IBaseRespostories<TEntity, TKey> where TEntity : class
        {
            protected readonly DbContext dbContext;
            public BaseRespostories(DbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<TEntity> AddAsync(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                await dbContext.Set<TEntity>().AddAsync(entity);
                return entity;
            }

            public async Task AddRangeAsync(IEnumerable<TEntity> entities)
            {
                if (entities == null || !entities.Any())
                {
                    return;
                }

                foreach (var entity in entities)
                {
                    await AddAsync(entity);
                }
            }

            public void Update(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                dbContext.Entry(entity).State = EntityState.Modified;
            }

            public void UpdateRange(IEnumerable<TEntity> entities)
            {
                foreach (var entity in entities)
                {
                    Update(entity);
                }
            }

            public void Remove(TEntity entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                dbContext.Set<TEntity>().Remove(entity);
            }

            public void RemoveRange(IEnumerable<TEntity> entities)
            {
                dbContext.Set<TEntity>().RemoveRange(entities);
            }

            public IQueryable<TEntity> GetQueryable()
            {
                return dbContext.Set<TEntity>().AsQueryable();
            }

            public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate)
            {
                return dbContext.Set<TEntity>().Where(predicate);
            }

            public IEnumerable<TEntity> GetList()
            {
                return dbContext.Set<TEntity>().AsEnumerable();
            }

            public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
            {
                return dbContext.Set<TEntity>().Where(predicate);
            }

            public async Task<TEntity> GetAsync(TKey id)
            {
                return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Equals(id));
            }

            public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            }

            public async Task<IEnumerable<TEntity>> GetListAsync()
            {
                return await dbContext.Set<TEntity>().ToListAsync();
            }

            public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await dbContext.Set<TEntity>().Where(predicate).ToListAsync();
            }

            public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await dbContext.Set<TEntity>().Where(predicate).CountAsync();
            }

            public async Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await dbContext.Set<TEntity>().Where(predicate).LongCountAsync();
            }

            public async Task<bool> AnyAsync()
            {
                return await dbContext.Set<TEntity>().AnyAsync();
            }

            public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await dbContext.Set<TEntity>().AnyAsync(predicate);
            }
        }
}
