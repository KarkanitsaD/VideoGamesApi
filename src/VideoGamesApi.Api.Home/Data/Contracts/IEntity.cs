namespace VideoGamesApi.Api.Home.Data.Contracts
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
