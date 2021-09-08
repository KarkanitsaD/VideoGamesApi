using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Business;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Mapping;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Models;
using Xunit;

namespace VideoGamesApi.Api.Home.IntegrationTests
{
    public class GenreServiceTests
    {
        private readonly IBusinessMapper _mapper;
        private IGenreService _service;

        public GenreServiceTests()
        {
            _mapper = new BusinessMapper(new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessMappingProfile());
            })));
        }

        [Theory]
        [MemberData(nameof(GetAsyncData))]
        public async Task GetAsync_PassQueryModel_ReturnsValidDtos(
            Collection<GenreEntity> allEntities,
            GenreQueryModel queryModel,
            int expectedDtoId)
        {
            //Arrange
            GenreDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<GenreEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new GenreService(new UnitOfWork(db), _mapper);

                dto = await _service.GetAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedDtoId, dto.Id);
        }

        [Theory]
        [MemberData(nameof(GetListAsyncData))]
        public async Task GetListAsync_PassQueryModel_ReturnsValidDtos(
            Collection<GenreEntity> allEntities,
            GenreQueryModel queryModel,
            int expectedCollectionSize)
        {
            //Arrange
            GenreDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<GenreEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            List<GenreDto> dtos;

            //Action
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new GenreService(new UnitOfWork(db), _mapper);

                dtos = (List<GenreDto>)await _service.GetListAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedCollectionSize, dtos.Count);
        }

        [Theory]
        [MemberData(nameof(RemoveAsyncData))]
        public async Task DeleteAsync_CheckSuccess(
            Collection<GenreEntity> allEntities,
            int entityIdToDelete,
            GenreDto expectedDto)
        {
            //Arrange
            GenreDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<GenreEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new GenreService(new UnitOfWork(db), _mapper);

                dto = await _service.RemoveAsync(entityIdToDelete);
            }

            //Assert
            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.Title, dto.Title);
        }

        [Theory]
        [MemberData(nameof(CreateAsyncData))]
        public async Task CreateAsync_CheckSuccess(
            Collection<GenreEntity> allEntities,
            GenreDto expectedDto)
        {
            //Arrange
            GenreDto createdDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<GenreEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new GenreService(new UnitOfWork(db), _mapper);

                createdDto = await _service.CreateAsync(expectedDto);
            }

            //Assert
            Assert.Equal(expectedDto.Title, createdDto.Title);
        }

        [Fact]
        public async Task Remove_NotExistedEntity_ThrowsKeyNotFoundException()
        {
            //Arrange
            var items = new Collection<GenreEntity>
            {
                new GenreEntity()
                {
                    Id = 1
                },
                new GenreEntity()
                {
                    Id = 2
                }
            };

            await using var db = new Context(GetContextOptionsBuilder().Options);
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            await db.Set<GenreEntity>().AddRangeAsync(items);
            await db.SaveChangesAsync();

            //Act & Assert
            await using var dbToDelete = new Context(GetContextOptionsBuilder().Options);
            _service = new GenreService(new UnitOfWork(dbToDelete), _mapper);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.RemoveAsync(33));
        }

        [Theory]
        [MemberData(nameof(ModifyAsyncData))]
        public async Task ModifyAsync_CheckSuccess(
            Collection<GenreEntity> allEntities,
            GenreDto entityToUpdate)
        {
            //Arrange
            GenreDto updatedDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<GenreEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new GenreService(new UnitOfWork(db), _mapper);

                await _service.Modify(entityToUpdate);

                var queryModel = new GenreQueryModel()
                {
                    Id = entityToUpdate.Id
                };

                updatedDto = await _service.GetAsync(queryModel);
            }

            Assert.Equal(entityToUpdate.Id, updatedDto.Id);
            Assert.Equal(entityToUpdate.Title, updatedDto.Title);
        }

        public static IEnumerable<object[]> GetListAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title1"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title2"
                        },
                        new GenreEntity()
                        {
                            Id = 4,
                            Title = "NotTitle"
                        },
                    },
                    new GenreQueryModel()
                    {
                        Title = "Title1"
                    },
                    2
                }
            };

            return data;
        }

        public static IEnumerable<object[]> GetAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Empty",
                        }
                    },
                    new GenreQueryModel()
                    {
                        Title = "Title"
                    },
                    1
                }
            };

            return data;
        }

        public static IEnumerable<object[]> RemoveAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        }
                    },
                    2,
                    new GenreDto()
                    {
                        Id = 2,
                        Title = "Title2"
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> CreateAsyncData()
        {
            var data = new List<object[]>()
            {
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new GenreDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new GenreDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new GenreEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new GenreDto()
                    {
                        Title = "Wow",
                    }
                },
            };

            return data;
        }

        public static IEnumerable<object[]> ModifyAsyncData()
        {
            var data = new List<object[]>()
            {
                new object[]
                {
                    new Collection<GenreEntity>
                    {
                        new GenreEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new GenreEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new GenreEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        },
                        new GenreEntity()
                        {
                            Id = 4,
                            Title = "Title4"
                        }
                    },
                    new GenreDto()
                    {
                        Id = 3,
                        Title = "Title333"
                    }
                }
            };

            return data;
        }

        private DbContextOptionsBuilder GetContextOptionsBuilder()
        {
            return new DbContextOptionsBuilder().UseInMemoryDatabase("GenreServiceTests");
        }
    }
}
