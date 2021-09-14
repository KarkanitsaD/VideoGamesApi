using System.Collections.Generic;
using VideoGamesApi.Api.Home.Data.Query;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Data.Query
{
    public class SortRuleTests
    {
        private SortRule<FakeEntity<int>, int> _sortRule;

        [Theory]
        [MemberData(nameof(GetData))]
        public void CheckSortValidation(SortRule<FakeEntity<int>, int> sortRule, bool isValid)
        {
            //Arrange
            _sortRule = sortRule;

            //Act & Assert
            Assert.Equal(isValid, _sortRule.IsValid);
        }

        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>
            {
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>(),
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        Order = SortOrder.Ascending
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        Expression = entity => entity.Id
                    },
                    false
                },
                new object[]
                {
                    new SortRule<FakeEntity<int>, int>
                    {
                        Order = SortOrder.Descending,
                        Expression = entity => entity.Id
                    },
                    true
                }
            };

            return list;
        }
    }
}
