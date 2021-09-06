using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Data
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly DbSet<TEntity> _set;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _set = context.Set<TEntity>();
            _context = context;
        }

        public TEntity Get(QueryParameters<TEntity, TKey> parameters)
        {
            return Query(parameters).FirstOrDefault();
        }

        public async Task<TEntity> GetAsync(QueryParameters<TEntity, TKey> parameters)
        {
            return await Query(parameters).FirstOrDefaultAsync();
        }

        public IList<TEntity> GetList(QueryParameters<TEntity, TKey> parameters = null)
        {
            return Query(parameters).ToList();
        }

        public async Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TKey> parameters = null)
        {
            return await Query(parameters).ToListAsync();
        }

        public PageResult<TEntity> GetPageList(QueryParameters<TEntity, TKey> parameters)
        {
            VerifyPageRule(parameters);

            var items = Query(parameters).ToList();

            var pageResult = new PageResult<TEntity>()
            {
                PageIndex = parameters.PageRule.Index,
                PageSIze = parameters.PageRule.Size,
                CountItems = items.Count,
                Items = items
            };

            return pageResult;
        }

        public async Task<PageResult<TEntity>> GetPageListAsync(QueryParameters<TEntity, TKey> parameters)
        {
            VerifyPageRule(parameters);

            var items = await Query(parameters).ToListAsync();

            var pageResult = new PageResult<TEntity>()
            {
                PageIndex = parameters.PageRule.Index,
                PageSIze = parameters.PageRule.Size,
                CountItems = items.Count,
                Items = items
            };

            return pageResult;
        }

        public bool Exists(QueryParameters<TEntity, TKey> parameters)
        {
            return Query(parameters).Any();
        }

        public async Task<bool> ExistsAsync(QueryParameters<TEntity, TKey> parameters)
        {
            return await Query(parameters).AnyAsync();
        }

        public TEntity Update(TEntity entity)
        {
            //TODO: read more about update
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            return _set.Add(entity).Entity;
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await _set.AddAsync(entity);
            return entityEntry.Entity;
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        public TEntity Delete(TEntity entity)
        {
            return _set.Remove(entity).Entity;
        }

        public int Count(QueryParameters<TEntity, TKey> parameters = null)
        {
            return Query(parameters).Count();
        }

        public async Task<int> CountAsync(QueryParameters<TEntity, TKey> parameters = null)
        {
            return await Query(parameters).CountAsync();
        }

        protected IQueryable<TEntity> Query(QueryParameters<TEntity, TKey> queryParameters)
        {
            var query = _set.AsQueryable();

            if (queryParameters == null)
                return query;

            if (queryParameters.FilterRule != null && queryParameters.FilterRule.IsValid)
            {
                query = query.Where(queryParameters.FilterRule.Expression);
            }

            if (queryParameters.SortRule != null && queryParameters.SortRule.IsValid)
            {
                query = queryParameters.SortRule.Order == SortOrder.Descending ?
                    query.OrderByDescending(queryParameters.SortRule.Expression) :
                    query.OrderBy(queryParameters.SortRule.Expression);
            }

            if (queryParameters.PageRule != null && queryParameters.PageRule.IsValid)
                query = query.Skip(queryParameters.PageRule.Size * queryParameters.PageRule.Index).Take(queryParameters.PageRule.Size);

            return query;
        }

        protected void VerifyPageRule(QueryParameters<TEntity, TKey> queryParameters)
        {
            if (queryParameters == null || queryParameters.PageRule == null || !queryParameters.PageRule.IsValid)
                throw new ArgumentException();
        }
    }
}
