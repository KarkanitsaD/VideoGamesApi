using VideoGamesApi.Api.Home.Business.Contracts;

namespace VideoGamesApi.Api.Home.Business.Models
{
    public abstract class Dto<TKey> : IDto<TKey>
    {
        public TKey Id { get; set; }
    }
}
