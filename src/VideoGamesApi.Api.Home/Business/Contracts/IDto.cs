namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IDto<TKey>
    {
        TKey Id { get; set; }
    }
}
