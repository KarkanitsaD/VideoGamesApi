using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Tests.Data.Fakes
{
    public class FakeEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
