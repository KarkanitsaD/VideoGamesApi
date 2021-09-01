using System;
using System.Linq.Expressions;
using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Data.Query
{
    public class FilterRule<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Expression { get; set; }

        public bool IsValid => Expression != null;
    }
}
