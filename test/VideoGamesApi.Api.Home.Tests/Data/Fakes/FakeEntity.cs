using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Tests.Data.Fakes
{
    public class FakeEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public string FakeFieldOne { get; set; }

        public static bool CompareEntities(FakeEntity<TKey> first, FakeEntity<TKey> second)
        {
            if (first == null && second == null)
                return true;

            if (first == null)
                return false;

            if (second == null)
                return false;

            return ReferenceEquals(first, second)
                   || first.Id.Equals(second.Id) && first.FakeFieldOne == second.FakeFieldOne;
        }

        public override bool Equals(object? obj)
        {
            var entity = (FakeEntity<TKey>)obj;

            if (entity == null)
                return false;
            return ReferenceEquals(this, entity)
                   || Id.Equals(entity.Id) && FakeFieldOne == entity.FakeFieldOne;

        }
    }
}
