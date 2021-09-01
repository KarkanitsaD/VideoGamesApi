using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Data.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        TEntity Get(QueryParameters<TEntity, TKey> parameters);

        Task<TEntity> GetAsync(QueryParameters<TEntity, TKey> parameters);

        IList<TEntity> GetList(QueryParameters<TEntity, TKey> parameters = null);

        Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TKey> parameters = null);

        PageResult<TEntity> GetPageList(QueryParameters<TEntity, TKey> parameters);

        Task<PageResult<TEntity>> GetPageListAsync(QueryParameters<TEntity, TKey> parameters);

        bool Exists(QueryParameters<TEntity, TKey> parameters);

        Task<bool> ExistsAsync(QueryParameters<TEntity, TKey> parameters);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        TEntity Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        int Count(QueryParameters<TEntity, TKey> parameters = null);

        Task<int> CountAsync(QueryParameters<TEntity, TKey> parameters = null);
    }
}
