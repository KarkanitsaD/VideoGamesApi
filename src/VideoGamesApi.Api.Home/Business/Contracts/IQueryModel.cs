namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IQueryModel<TKey>
    {
        TKey? Id { get; set; }

        int Index { get; set; }

        int Size { get; set; }

        bool IsValidPageModel { get; }

        SortOrder SortOrder { get; set; }
    }

    public enum SortOrder
    {
        Ascending = 0,
        Descending = 0
    }
}
