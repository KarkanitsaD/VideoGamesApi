using System;
using System.Linq.Expressions;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data.Query
{
    public class SortRule<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public SortOrder? Order { get; set; }

        public Expression<Func<TEntity, object>> Expression { get; set; }

        public bool IsValid => Expression != null && Order != null;
    }
}
