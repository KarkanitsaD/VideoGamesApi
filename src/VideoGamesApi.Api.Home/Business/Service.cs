﻿using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Query;
using SortOrder = VideoGamesApi.Api.Home.Business.QueryModels.SortOrder;

namespace VideoGamesApi.Api.Home.Business
{
    public abstract class Service<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private SortRule<TEntity, TKey> GetSortRule(QueryModel model)
        {
            var sortRule = new SortRule<TEntity, TKey>();

            if (model.SortOrder == null)
                return sortRule;

            sortRule.Order = model.SortOrder == SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        protected abstract void DefineSortExpression(SortRule<TEntity, TKey> sortRule);

        private PageRule GetPageRule(QueryModel model)
        {
            var pageRule = new PageRule()
            {
                Index = model.Index,
                Size = model.Size
            };

            return pageRule;
        }

        protected abstract FilterRule<TEntity, TKey> GetFilterRule(QueryModel model);
    }
}
