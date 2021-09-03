using VideoGamesApi.Api.Home.Contracts;

namespace VideoGamesApi.Api.Home.Models
{
    public abstract class Model<TKey> : IModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
