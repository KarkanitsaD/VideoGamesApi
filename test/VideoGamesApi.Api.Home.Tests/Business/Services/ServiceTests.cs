using System.Collections.Generic;
using VideoGamesApi.Api.Home.Data.Query;
using VideoGamesApi.Api.Home.Tests.Business.Fakes;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;
using SortOrder = VideoGamesApi.Api.Home.Business.QueryModels.SortOrder;

namespace VideoGamesApi.Api.Home.Tests.Business.Services
{
    public class ServiceTests : FakeService
    {
        public static IEnumerable<object[]> SortData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new FakeQueryModel(),
                    new SortRule<FakeEntity<int>, int>()
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = null
                    },
                    new SortRule<FakeEntity<int>, int>()
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = SortOrder.Ascending
                    },
                    new SortRule<FakeEntity<int>, int>
                    {
                        Order = Home.Data.Query.SortOrder.Ascending,
                        Expression = fake => fake.Id
                    }
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = SortOrder.Descending
                    },
                    new SortRule<FakeEntity<int>, int>
                    {
                        Order = Home.Data.Query.SortOrder.Descending,
                        Expression = fake => fake.Id
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> PageData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new FakeQueryModel(),
                    new PageRule()
                },
                new object[]
                {
                    new FakeQueryModel()
                    {
                        Index = 2,
                        Size = 12
                    },
                    new PageRule()
                    {
                        Index = 2,
                        Size = 12
                    }
                }

            };

            return data;
        }

        [Theory]
        [MemberData(nameof(SortData))]
        public void GetSortRule_Test(FakeQueryModel model, SortRule<FakeEntity<int>, int> expectedSortRule)
        {
            //Arrange & Act
            var parameters = GetQueryParameters(model);

            //Assert
            Assert.Equal(expectedSortRule.IsValid, parameters.SortRule.IsValid);
            Assert.Equal(expectedSortRule.Order, parameters.SortRule.Order);
        }

        [Theory]
        [MemberData(nameof(PageData))]
        public void GetPageRule_Test(FakeQueryModel model, PageRule expectedPageRule)
        {
            //Arrange & Act
            var parameters = GetQueryParameters(model);

            //Assert
            Assert.Equal(expectedPageRule.IsValid, parameters.PageRule.IsValid);
            Assert.Equal(expectedPageRule.Index, parameters.PageRule.Index);
            Assert.Equal(expectedPageRule.Size, parameters.PageRule.Size);
        }
    }
}
