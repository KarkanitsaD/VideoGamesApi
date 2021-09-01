using VideoGamesApi.Api.Home.Business.Contracts;

namespace VideoGamesApi.Api.Home.Business.QueryModels
{
    public abstract class QueryModel<TKey> : IQueryModel<TKey>
    {
        public TKey Id { get; set; }

        public int Index { get; set; }

        public int Size { get; set; }

        public bool IsValidPageModel => Size > 0 && Index >= 0;

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    }
}
