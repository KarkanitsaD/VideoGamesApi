using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Data.Models
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
