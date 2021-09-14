namespace VideoGamesApi.Api.Home.Contracts
{
    public interface IModel<TKey>
    {
        TKey Id { get; set; }
    }
}
