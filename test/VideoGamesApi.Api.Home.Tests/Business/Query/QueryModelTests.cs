using System.Collections.Generic;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Tests.Business.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Business.Query
{
    public class QueryModelTests
    {
        public static IEnumerable<object[]> ValidationPageModelData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new FakeQueryModel(),
                    false
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        Index = -1,
                        Size = 22
                    },
                    false
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        Index = 0,
                        Size = 0
                    },
                    false
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        Index = 12,
                        Size = 13
                    },
                    true
                }
            };

            return data;
        }

        public static IEnumerable<object[]> ValidationSortModelData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = null
                    },
                    false
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = SortOrder.Ascending
                    },
                    true
                },
                new object[]
                {
                    new FakeQueryModel
                    {
                        SortOrder = SortOrder.Descending
                    },
                    true
                }
            };

            return data;
        }

        [Theory]
        [MemberData(nameof(ValidationPageModelData))]
        public void CheckPageValidation(FakeQueryModel model, bool isValid)
        {
            //Arrange & Act & Assert
            Assert.Equal(model.IsValidPageModel, isValid);
        }

        [Theory]
        [MemberData(nameof(ValidationSortModelData))]
        public void CheckSortValidation(FakeQueryModel model, bool isValid)
        {
            //Arrange & Act & Assert
            Assert.Equal(model.IsValidSortModel, isValid);

        }
    }
}
