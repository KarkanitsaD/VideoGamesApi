using System;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Query;
using VideoGamesApi.Api.Home.Data.Repositories;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.Tests.Data.Repository
{
    public class RepositoryTests
    {
        private readonly Mock<DbContext> _mockDbContext = new Mock<DbContext>();

        private static readonly Collection<FakeEntity<int>> Entities = new()
        {
            new() { Id = 2, FakeFieldOne = "FakeFieldOne" },
            new() { Id = 3, FakeFieldOne = "FakeFieldOne" },
            new() { Id = 4, FakeFieldOne = "FakeFieldTwo" },
            new() { Id = 5, FakeFieldOne = "Not" },
            new() { Id = 6, FakeFieldOne = "Fake" },
            new() { Id = 7, FakeFieldOne = "Not7" },
            new() { Id = 8, FakeFieldOne = "Not8" },
            new() { Id = 9, FakeFieldOne = "Not9" },
            new() { Id = 10, FakeFieldOne = "Not110" },
            new() { Id = 1, FakeFieldOne = "" },
        };

        private Repository<TEntity, TKey> GetRepository<TEntity, TKey>(ICollection<TEntity> items)
            where TEntity : class, IEntity<TKey>
        {
            var mockData = items.AsQueryable().BuildMock();

            var mockDbSet = mockData.Object.BuildMockDbSet();

            _mockDbContext.Setup(x => x.Set<TEntity>())
                .Returns(mockDbSet.Object);

            return new Repository<TEntity, TKey>(_mockDbContext.Object);
        }

        [Theory]
        [MemberData(nameof(FilterQueryData))]
        public async Task GetListAsync_CheckFilterRule(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            IQueryable<FakeEntity<int>> expectedEntities,
            int expectedSize)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var entities = await repository.GetListAsync(parameters);

            //Assert
            Assert.Equal(expectedSize, entities.Count);
            Assert.Equal(expectedEntities, entities);
        }

        [Theory]
        [MemberData(nameof(FilterQueryData))]
        public void GetList_CheckFilterRule(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            IQueryable<FakeEntity<int>> expectedEntities,
            int expectedSize)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var entities = repository.GetList(parameters);

            //Assert
            Assert.Equal(expectedSize, entities.Count);
            Assert.Equal(expectedEntities, entities);
        }

        [Theory]
        [MemberData(nameof(FilterQueryData))]
        public async Task CountAsync_CountItems(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            IQueryable<FakeEntity<int>> expectedEntities,
            int expectedSize)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var countEntities = await repository.CountAsync(parameters);

            //Assert
            Assert.Equal(expectedSize, countEntities);
            Assert.Equal(expectedEntities.Count(), countEntities);
        }

        [Theory]
        [MemberData(nameof(FilterQueryData))]
        public void Count_CountItems(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            IQueryable<FakeEntity<int>> expectedEntities,
            int expectedSize)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var countEntities = repository.Count(parameters);

            //Assert
            Assert.Equal(expectedSize, countEntities);
            Assert.Equal(expectedEntities.Count(), countEntities);
        }

        [Theory]
        [MemberData(nameof(SortQueryData))]
        public async Task GetAsync_CheckSortData(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            IQueryable<FakeEntity<int>> expectedEntities,
            int expectedFirsElementId)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var entities = await repository.GetListAsync(parameters);

            //Assert
            Assert.Equal(expectedFirsElementId, entities[0].Id);
            Assert.Equal(expectedEntities, entities);
        }

        [Theory]
        [MemberData(nameof(PageQueryData))]
        public async Task GetPageListAsync_CheckPageResult(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            PageResult<FakeEntity<int>> expectedResult)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var pageResult = await repository.GetPageListAsync(parameters);

            //Assert
            Assert.Equal(expectedResult.CountItems, pageResult.CountItems);
            Assert.Equal(expectedResult.PageSIze, pageResult.PageSIze);
            Assert.Equal(expectedResult.PageIndex, pageResult.PageIndex);
            Assert.Equal(expectedResult.Items, pageResult.Items);
        }

        [Theory]
        [MemberData(nameof(PageQueryData))]
        public void GetPageList_CheckPageResult(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            PageResult<FakeEntity<int>> expectedResult)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var pageResult = repository.GetPageList(parameters);

            //Assert
            Assert.Equal(expectedResult.CountItems, pageResult.CountItems);
            Assert.Equal(expectedResult.PageSIze, pageResult.PageSIze);
            Assert.Equal(expectedResult.PageIndex, pageResult.PageIndex);
            Assert.Equal(expectedResult.Items, pageResult.Items);
        }

        [Theory]
        [MemberData(nameof(ExistsData))]
        public async Task ExistsAsync_Test(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            bool expectedExists)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var exists = await repository.ExistsAsync(parameters);

            //Assert
            Assert.Equal(expectedExists, exists);
        }

        [Theory]
        [MemberData(nameof(ExistsData))]
        public void Exists_Test(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            bool expectedExists)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var exists = repository.Exists(parameters);

            //Assert
            Assert.Equal(expectedExists, exists);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task GetAsync_Test(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            FakeEntity<int> expectedEntity)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var entity = await repository.GetAsync(parameters);

            //Assert
            Assert.Equal(expectedEntity, entity);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Get_Test(
            QueryParameters<FakeEntity<int>, int> parameters,
            Collection<FakeEntity<int>> allEntities,
            FakeEntity<int> expectedEntity)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(allEntities);

            //Act
            var entity = repository.Get(parameters);

            //Assert
            Assert.Equal(expectedEntity, entity);
        }

        [Theory]
        [MemberData(nameof(PageQueryExceptionData))]
        public async Task GetPageListAsync_ThrowsArgumentException(QueryParameters<FakeEntity<int>, int> parameters)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(new List<FakeEntity<int>>());

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () =>
                    repository.GetPageListAsync(parameters)
            );
        }

        [Theory]
        [MemberData(nameof(PageQueryExceptionData))]
        public void GetPageList_ThrowsArgumentException(QueryParameters<FakeEntity<int>, int> parameters)
        {
            //Arrange
            var repository = GetRepository<FakeEntity<int>, int>(new List<FakeEntity<int>>());

            //Act & Assert
            Assert.Throws<ArgumentException>(
                () =>
                {
                    repository.GetPageList(parameters);
                }
            );
        }

        public static IEnumerable<object[]> FilterQueryData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    null,
                    Entities,
                    Entities.AsQueryable(),
                    10
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>(),
                    Entities,
                    Entities.AsQueryable(),
                    10
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.FakeFieldOne == "FakeFieldOne"
                        }
                    },
                    Entities,
                    Entities.Where(fake => fake.FakeFieldOne == "FakeFieldOne").AsQueryable(),
                    2
                },
            };

            return data;
        }

        public static IEnumerable<object[]> SortQueryData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    null,
                    Entities,
                    Entities.AsQueryable(),
                    2
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        SortRule = new SortRule<FakeEntity<int>, int>()
                    },
                    Entities,
                    Entities.AsQueryable(),
                    2
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        SortRule = new SortRule<FakeEntity<int>, int>()
                        {
                            Expression =  fake => fake.Id
                        }
                    },
                    Entities,
                    Entities.AsQueryable(),
                    2
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        SortRule = new SortRule<FakeEntity<int>, int>()
                        {
                            Order = SortOrder.Ascending
                        }
                    },
                    Entities,
                    Entities.AsQueryable(),
                    2
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        SortRule = new SortRule<FakeEntity<int>, int>()
                        {
                            Expression = fake => fake.Id,
                            Order = SortOrder.Ascending
                        }
                    },
                    Entities,
                    Entities.OrderBy(fake => fake.Id).AsQueryable(),
                    1
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        SortRule = new SortRule<FakeEntity<int>, int>()
                        {
                            Expression = fake => fake.Id,
                            Order = SortOrder.Descending
                        }
                    },
                    Entities,
                    Entities.OrderByDescending(fake => fake.Id).AsQueryable(),
                    10
                }
            };

            return data;
        }

        public static IEnumerable<object[]> PageQueryData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule
                        {
                            Index = 0,
                            Size = 3
                        }
                    },
                    Entities,
                    new PageResult<FakeEntity<int>>
                    {
                        CountItems = Entities.Skip(0).Take(3).Count(),
                        PageIndex = 0,
                        PageSIze = 3,
                        Items = Entities.Skip(0).Take(3).AsQueryable()
                    }
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule
                        {
                            Index = 1,
                            Size = 3
                        }
                    },
                    Entities,
                    new PageResult<FakeEntity<int>>
                    {
                        CountItems = Entities.Skip(1*3).Take(3).Count(),
                        PageIndex = 1,
                        PageSIze = 3,
                        Items = Entities.Skip(1*3).Take(3).AsQueryable()
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> ExistsData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 5 && fake.FakeFieldOne == "Not"
                        }
                    },
                    Entities,
                    true
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 100 || fake.FakeFieldOne == "Not"
                        }
                    },
                    Entities,
                    true
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 100 && fake.FakeFieldOne == "Nooooot"
                        }
                    },
                    Entities,
                    false
                }
            };

            return data;
        }

        public static IEnumerable<object[]> GetData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 100
                        }
                    },
                    Entities,
                    null
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 1
                        }
                    },
                    Entities,
                    Entities[9]
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        FilterRule = new FilterRule<FakeEntity<int>, int>
                        {
                            Expression = fake => fake.Id == 1 || fake.Id == 2
                        }
                    },
                    Entities,
                    Entities[0]
                },

            };

            return data;
        }

        public static IEnumerable<object[]> PageQueryExceptionData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>()
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule()
                    }
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule
                        {
                            Index = -1,
                            Size = 12
                        }
                    }
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule
                        {
                            Index = 12,
                            Size = -1
                        }
                    }
                },
                new object[]
                {
                    new QueryParameters<FakeEntity<int>, int>
                    {
                        PageRule = new PageRule()
                        {
                            Index = 1,
                            Size = 0
                        }
                    }
                }
            };

            return data;
        }
    }
}