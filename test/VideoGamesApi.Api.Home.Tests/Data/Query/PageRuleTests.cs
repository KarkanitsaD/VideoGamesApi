using System.Collections.Generic;
using VideoGamesApi.Api.Home.Data.Query;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Data.Query
{
    public class PageRuleTests
    {
        private PageRule _pageRule;

        [Theory]
        [MemberData(nameof(GetData))]
        public void CheckPageValidation(PageRule pageRule, bool isValid)
        {
            //Arrange
            _pageRule = pageRule;

            //Act & Assert
            Assert.Equal(isValid, _pageRule.IsValid);

        }


        public static IEnumerable<object[]> GetData()
        {
            var pageRules = new List<object[]>()
            {
                new object[]
                {
                    new PageRule(),
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = -1,
                        Size = 22
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = 0,
                        Size = 0
                    },
                    false
                },
                new object[]
                {
                    new PageRule
                    {
                        Index = 12,
                        Size = 13
                    },
                    true
                }
            };

            return pageRules;
        }
    }
}
