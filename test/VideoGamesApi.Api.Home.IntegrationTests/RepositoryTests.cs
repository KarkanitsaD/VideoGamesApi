using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Repositories;
using VideoGamesApi.Api.Home.IntegrationTests.Fakes;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;
using Xunit;

namespace VideoGamesApi.Api.Home.IntegrationTests
{
    public class RepositoryTests
    {
        private IRepository<FakeEntity<int>, int> _repository;
        private readonly FakeDbContext _context;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FakeDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            _context = new FakeDbContext(options);
        }

        [Theory]
        [MemberData(nameof(UpdateData))]
        public void Update_Test(
            Collection<FakeEntity<int>> allEntities, 
            FakeEntity<int> entityToUpdate, 
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            
            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            _repository.Update(entityToUpdate);
            _context.SaveChanges();

            var entities = _context.Set<FakeEntity<int>>().ToList();
            
            //Assert
            Assert.Equal(expectedEntities.ToList(), entities);
        }

        [Theory]
        [MemberData(nameof(DeleteData))]
        public void Delete_Test(
            Collection<FakeEntity<int>> allEntities,
            FakeEntity<int> entityToDelete,
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            _repository.Delete(entityToDelete);
            _context.SaveChanges();

            var entities = _context.Set<FakeEntity<int>>().ToList();

            //Assert
            Assert.Equal(expectedEntities.ToList(), entities);

        }

        [Theory]
        [MemberData(nameof(InsertListData))]
        public async Task InsertListAsync_Test(
            Collection<FakeEntity<int>> allEntities,
            Collection<FakeEntity<int>> entitiesToInsert,
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            await _repository.InsertAsync(entitiesToInsert);

            await _context.SaveChangesAsync();

            var entities = await _repository.GetListAsync();

            //Assert
            Assert.Equal(expectedEntities.ToList(), entities.ToList());
        }

        [Theory]
        [MemberData(nameof(InsertListData))]
        public void InsertList_Test(
            Collection<FakeEntity<int>> allEntities,
            Collection<FakeEntity<int>> entitiesToInsert,
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            _repository.Insert(entitiesToInsert);

            _context.SaveChanges();

            var entities = _repository.GetList().ToList();

            //Assert
            Assert.Equal(expectedEntities.ToList(), entities);
        }

        [Theory]
        [MemberData(nameof(InsertData))]
        public async Task InsertAsync_Test(
            Collection<FakeEntity<int>> allEntities,
            FakeEntity<int> entityToInsert,
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            await _repository.InsertAsync(entityToInsert);

            await _context.SaveChangesAsync();

            var entities = await _repository.GetListAsync();

            //Assert
            Assert.Equal(expectedEntities.ToList(), entities.ToList());
        }

        [Theory]
        [MemberData(nameof(InsertData))]
        public void Insert_Test(
            Collection<FakeEntity<int>> allEntities,
            FakeEntity<int> entityToInsert,
            Collection<FakeEntity<int>> expectedEntities)
        {
            //Arrange
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Set<FakeEntity<int>>()
                .AddRange(allEntities);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            _repository = new Repository<FakeEntity<int>, int>(_context);

            //Act
            _repository.Insert(entityToInsert);

            _context.SaveChanges();

            var entities = _repository.GetList().ToList();

            //Assert
            Assert.Equal(expectedEntities.ToList(), entities);
        }

        public static IEnumerable<object[]> UpdateData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    },
                    new FakeEntity<int>
                    {
                        Id = 1,
                        FakeFieldOne = "NotFirst"
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "NotFirst"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> DeleteData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3
                        }
                    },
                    new FakeEntity<int>
                    {
                        Id = 2,
                        FakeFieldOne = "Second"
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3
                        }
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> InsertListData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 3,
                            FakeFieldOne = "Third"
                        }
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3,
                            FakeFieldOne = "Third"
                        }
                    }
                },
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            FakeFieldOne = "Third"
                        }
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3,
                            FakeFieldOne = "Third"
                        }
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> InsertData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    },
                    new FakeEntity<int>
                    {
                        Id = 3,
                        FakeFieldOne = "Third"
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3,
                            FakeFieldOne = "Third"
                        }
                    }
                },
                new object[]
                {
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        }
                    },
                    new FakeEntity<int>
                    {
                        FakeFieldOne = "Third"
                    },
                    new Collection<FakeEntity<int>>
                    {
                        new FakeEntity<int>
                        {
                            Id = 1,
                            FakeFieldOne = "First"
                        },
                        new FakeEntity<int>
                        {
                            Id = 2,
                            FakeFieldOne = "Second"
                        },
                        new FakeEntity<int>
                        {
                            Id = 3,
                            FakeFieldOne = "Third"
                        }
                    }
                }
            };

            return data;
        }

    }
}
