using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Data.Query
{
    public class QueryParameters<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public FilterRule<TEntity, TKey> FilterRule { get; set; }

        public SortRule<TEntity, TKey> SortRule { get; set; }

        public PageRule PageRule { get; set; }
    }
}
