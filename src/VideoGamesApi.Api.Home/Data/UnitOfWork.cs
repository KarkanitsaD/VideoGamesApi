using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Repositories;

namespace VideoGamesApi.Api.Home.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private bool _disposed;
        private readonly Dictionary<string, object> _repositories;
        public UnitOfWork(Context context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class, IEntity<TKey>
        {
            var key = typeof(TEntity).ToString();
            if (!_repositories.ContainsKey(key))
                _repositories.Add(key, new Repository<TEntity, TKey>(_context));
            return (IRepository<TEntity, TKey>)_repositories[key];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_disposed)
            {
                return;
            }

            _context?.Dispose();
            _disposed = true;
        }
    }
}
