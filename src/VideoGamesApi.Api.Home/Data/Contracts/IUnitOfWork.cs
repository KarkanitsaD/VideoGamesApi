using System;
using System.Threading.Tasks;

namespace VideoGamesApi.Api.Home.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
    }
}
