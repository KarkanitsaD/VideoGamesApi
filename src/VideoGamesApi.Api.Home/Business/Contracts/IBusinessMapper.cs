namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IBusinessMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source);
    }
}
