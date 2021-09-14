using VideoGamesApi.Api.Home.Business;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Query;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;

namespace VideoGamesApi.Api.Home.Tests.Business.Fakes
{
    public class FakeService : Service<FakeEntity<int>, int>
    {
        protected override void DefineSortExpression(SortRule<FakeEntity<int>, int> sortRule)
        {
            sortRule.Expression = fake => fake.Id;
        }

        protected override FilterRule<FakeEntity<int>, int> GetFilterRule(QueryModel model)
        {
            return new();
        }
    }
}
