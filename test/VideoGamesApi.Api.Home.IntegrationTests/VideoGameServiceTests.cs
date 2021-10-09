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
    public class VideoGameServiceTests
    {
        private readonly IMapper _mapper;
        private IVideoGameService _service;

        public VideoGameServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessMappingProfile());
            }));
        }

        [Theory]
        [MemberData(nameof(GetAsyncData))]
        public async Task GetAsync_PassQueryModel_ReturnsValidDtos(
            Collection<VideoGameEntity> allEntities,
            VideoGameQueryModel queryModel,
            int expectedDtoId)
        {
            //Arrange
            VideoGameDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<VideoGameEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new VideoGameService(new UnitOfWork(db), _mapper);

                dto = await _service.GetAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedDtoId, dto.Id);
        }

        [Theory]
        [MemberData(nameof(GetListAsyncData))]
        public async Task GetListAsync_PassQueryModel_ReturnsValidDtos(
            Collection<VideoGameEntity> allEntities,
            VideoGameQueryModel queryModel,
            int expectedCollectionSize)
        {
            //Arrange
            List<VideoGameDto> dtos;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<VideoGameEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Action
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new VideoGameService(new UnitOfWork(db), _mapper);

                dtos = (List<VideoGameDto>)await _service.GetListAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedCollectionSize, dtos.Count);
        }

        [Theory]
        [MemberData(nameof(RemoveAsyncData))]
        public async Task DeleteAsync_CheckSuccess(
            Collection<VideoGameEntity> allEntities,
            int entityIdToDelete,
            VideoGameDto expectedDto)
        {
            //Arrange
            VideoGameDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<VideoGameEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new VideoGameService(new UnitOfWork(db), _mapper);

                dto = await _service.RemoveAsync(entityIdToDelete);
            }

            //Assert
            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.Title, dto.Title);
        }

        [Theory]
        [MemberData(nameof(CreateAsyncData))]
        public async Task CreateAsync_CheckSuccess(
            Collection<VideoGameEntity> allEntities,
            VideoGameDto expectedDto)
        {
            //Arrange
            VideoGameDto createdDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<VideoGameEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new VideoGameService(new UnitOfWork(db), _mapper);

                createdDto = await _service.CreateAsync(expectedDto);
            }

            //Assert
            Assert.Equal(expectedDto.Title, createdDto.Title);
        }

        [Fact]
        public async Task Remove_NotExistedEntity_ThrowsKeyNotFoundException()
        {
            //Arrange
            var items = new Collection<VideoGameEntity>
            {
                new VideoGameEntity()
                {
                    Id = 1
                },
                new VideoGameEntity()
                {
                    Id = 2
                }
            };

            await using var db = new Context(GetContextOptionsBuilder().Options);
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            await db.Set<VideoGameEntity>().AddRangeAsync(items);
            await db.SaveChangesAsync();

            //Act & Assert
            await using var dbToDelete = new Context(GetContextOptionsBuilder().Options);
            _service = new VideoGameService(new UnitOfWork(dbToDelete), _mapper);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.RemoveAsync(33));
        }

        [Theory]
        [MemberData(nameof(ModifyAsyncData))]
        public async Task ModifyAsync_CheckSuccess(
            Collection<VideoGameEntity> allEntities,
            VideoGameDto entityToUpdate)
        {
            //Arrange
            VideoGameDto updatedDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<VideoGameEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new VideoGameService(new UnitOfWork(db), _mapper);

                await _service.UpdateAsync(entityToUpdate);

                var queryModel = new VideoGameQueryModel()
                {
                    Id = entityToUpdate.Id
                };

                updatedDto = await _service.GetAsync(queryModel);
            }

            Assert.Equal(entityToUpdate.Id, updatedDto.Id);
            Assert.Equal(entityToUpdate.Title, updatedDto.Title);
            Assert.Equal(entityToUpdate.Rating, updatedDto.Rating);
            Assert.Equal(entityToUpdate.YearOfIssue, updatedDto.YearOfIssue);
        }

        public static IEnumerable<object[]> GetListAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title1"
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title2"
                        },
                        new VideoGameEntity()
                        {
                            Id = 4,
                            Title = "NotTitle"
                        },
                    },
                    new VideoGameQueryModel()
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
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Empty",
                        }
                    },
                    new VideoGameQueryModel()
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
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        }
                    },
                    2,
                    new VideoGameDto()
                    {
                        Id = 2,
                        Title = "Title2"
                    }
                }
            };

            return data;
        }

        public static IEnumerable<object[]> ModifyAsyncData()
        {
            var data = new List<object[]>()
            {
                new object[]
                {
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                            YearOfIssue = 2001,
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                            Rating = (float) 8.22
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title3",
                            YearOfIssue = 2012,
                            Rating = (float) 9.45
                        },
                        new VideoGameEntity()
                        {
                            Id = 4,
                            Title = "Title4"
                        }
                    },
                    new VideoGameDto()
                    {
                        Id = 3,
                        Title = "Title333",
                        YearOfIssue = 2006
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
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new VideoGameDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new VideoGameDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<VideoGameEntity>
                    {
                        new VideoGameEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new VideoGameEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new VideoGameDto()
                    {
                        Title = "Wow",
                    }
                },
            };

            return data;
        }

        private DbContextOptionsBuilder GetContextOptionsBuilder()
        {
            return new DbContextOptionsBuilder().UseInMemoryDatabase("VideoGameServiceTests");
        }
    }
}
