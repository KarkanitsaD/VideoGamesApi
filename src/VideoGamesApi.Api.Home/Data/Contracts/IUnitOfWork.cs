using System;
using System.Threading.Tasks;

namespace VideoGamesApi.Api.Home.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
