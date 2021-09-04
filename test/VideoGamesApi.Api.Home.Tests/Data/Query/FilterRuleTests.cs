using System.Collections.Generic;
using VideoGamesApi.Api.Home.Data.Query;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Data.Query
{
    public class FilterRuleTests
    {
        private FilterRule<FakeEntity<int>, int> _filterRule;

        [Theory]
        [MemberData(nameof(GetData))]
        public void CheckFilterValidation(FilterRule<FakeEntity<int>, int> filterRule, bool isValid)
        {
            //Arrange
            _filterRule = filterRule;

            //Act & Assert
            Assert.Equal(isValid, _filterRule.IsValid);
        }

        public static IEnumerable<object[]> GetData()
        {
            var list = new List<object[]>
            {
                new object[]
                {
                    new FilterRule<FakeEntity<int>, int>(),
                    false
                },
                new object[]
                {
                    new FilterRule<FakeEntity<int>, int>
                    {
                        Expression = null
                    },
                    false
                },
                new object[]
                {
                    new FilterRule<FakeEntity<int>, int>
                    {
                        Expression = entity => entity.Id == 1
                    },
                    true
                }
            };

            return list;
        }
    }
}
