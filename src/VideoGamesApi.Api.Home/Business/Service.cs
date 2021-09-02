using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Query;
using SortOrder = VideoGamesApi.Api.Home.Business.QueryModels.SortOrder;

namespace VideoGamesApi.Api.Home.Business
{
    public abstract class Service
    {
        
        private SortRule<TEntity, TKey> GetSortRule<TEntity, TKey>(QueryModel model) where TEntity : class, IEntity<TKey>
        {
            var sortRule = new SortRule<TEntity, TKey>
            {
                Order = model.SortOrder == SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending
            };
            DefineSortExpression(model, sortRule);

            return sortRule;
        }

        protected abstract void DefineSortExpression<TEntity, TKey>(QueryModel model, SortRule<TEntity, TKey> sortRule)
            where TEntity : class, IEntity<TKey>;

        private PageRule GetPageRule(QueryModel model)
        {
            var pageRule = new PageRule()
            {
                Index = model.Index,
                Size = model.Size
            };

            return pageRule;
        }

        protected abstract FilterRule<TEntity, TKey> GetFilterRule<TEntity, TKey>(QueryModel model)
            where TEntity : class, IEntity<TKey>;
    }
}
