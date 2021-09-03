namespace VideoGamesApi.Api.Home.Contracts
{
    public interface IPresentationMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
