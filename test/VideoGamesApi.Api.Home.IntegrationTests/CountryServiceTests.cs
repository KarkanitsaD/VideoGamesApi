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
    public class CountryServiceTests
    {
        private readonly IMapper _mapper;
        private ICountryService _service;

        public CountryServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessMappingProfile());
            }));
        }

        [Theory]
        [MemberData(nameof(GetAsyncData))]
        public async Task GetAsync_PassQueryModel_ReturnsValidDtos(
            Collection<CountryEntity> allEntities,
            CountryQueryModel queryModel,
            int expectedDtoId)
        {
            //Arrange
            CountryDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CountryEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CountryService(new UnitOfWork(db), _mapper);

                dto = await _service.GetAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedDtoId, dto.Id);
        }

        [Theory]
        [MemberData(nameof(GetListAsyncData))]
        public async Task GetListAsync_PassQueryModel_ReturnsValidDtos(
            Collection<CountryEntity> allEntities,
            CountryQueryModel queryModel,
            int expectedCollectionSize)
        {
            //Arrange
            List<CountryDto> dtos;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CountryEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Action
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CountryService(new UnitOfWork(db), _mapper);

                dtos = (List<CountryDto>)await _service.GetListAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedCollectionSize, dtos.Count);
        }

        [Theory]
        [MemberData(nameof(RemoveAsyncData))]
        public async Task RemoveAsync_CheckSuccess(
            Collection<CountryEntity> allEntities,
            int entityIdToDelete,
            CountryDto expectedDto)
        {
            //Arrange
            CountryDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CountryEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CountryService(new UnitOfWork(db), _mapper);

                dto = await _service.RemoveAsync(entityIdToDelete);
            }

            //Assert
            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.Title, dto.Title);
        }

        [Theory]
        [MemberData(nameof(CreateAsyncData))]
        public async Task CreateAsync_CheckSuccess(
            Collection<CountryEntity> allEntities,
            CountryDto expectedDto)
        {
            //Arrange
            CountryDto createdDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CountryEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CountryService(new UnitOfWork(db), _mapper);

                createdDto = await _service.CreateAsync(expectedDto);
            }

            //Assert
            Assert.Equal(expectedDto.Title, createdDto.Title);
        }

        [Theory]
        [MemberData(nameof(ModifyAsyncData))]
        public async Task ModifyAsync_CheckSuccess(
            Collection<CountryEntity> allEntities,
            CountryDto entityToUpdate)
        {
            //Arrange
            CountryDto updatedDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CountryEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CountryService(new UnitOfWork(db), _mapper);

                await _service.UpdateAsync(entityToUpdate);

                var queryModel = new CountryQueryModel()
                {
                    Id = entityToUpdate.Id
                };

                updatedDto = await _service.GetAsync(queryModel);
            }

            Assert.Equal(entityToUpdate.Id, updatedDto.Id);
            Assert.Equal(entityToUpdate.Title, updatedDto.Title);
        }

        [Fact]
        public async Task Remove_NotExistedEntity_ThrowsKeyNotFoundException()
        {
            //Arrange
            var items = new Collection<CountryEntity>
            {
                new CountryEntity()
                {
                    Id = 1
                },
                new CountryEntity()
                {
                    Id = 2
                }
            };

            await using var db = new Context(GetContextOptionsBuilder().Options);
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            await db.Set<CountryEntity>().AddRangeAsync(items);
            await db.SaveChangesAsync();

            //Act & Assert
            await using var dbToDelete = new Context(GetContextOptionsBuilder().Options);
            _service = new CountryService(new UnitOfWork(dbToDelete), _mapper);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.RemoveAsync(33));
        }

        public static IEnumerable<object[]> GetListAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title1"
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title2"
                        },
                        new CountryEntity()
                        {
                            Id = 4,
                            Title = "NotTitle"
                        },
                    },
                    new CountryQueryModel()
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
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Empty",
                        }
                    },
                    new CountryQueryModel()
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
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                            Companies = new List<CompanyEntity>()
                            {
                                new CompanyEntity()
                                {
                                    Title = "Company1"
                                },
                                new CompanyEntity()
                                {
                                    Title = "Company2"
                                },
                            }
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        }
                    },
                    2,
                    new CountryDto()
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
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        },
                        new CountryEntity()
                        {
                            Id = 4,
                            Title = "Title4"
                        }
                    },
                    new CountryDto()
                    {
                        Id = 3,
                        Title = "Title333"
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
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CountryDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CountryDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<CountryEntity>
                    {
                        new CountryEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CountryEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CountryDto()
                    {
                        Title = "Wow",
                    }
                },
            };

            return data;
        }

        private DbContextOptionsBuilder GetContextOptionsBuilder()
        {
            return new DbContextOptionsBuilder().UseInMemoryDatabase("CountryServiceTestsDb");
        }
    }
}
