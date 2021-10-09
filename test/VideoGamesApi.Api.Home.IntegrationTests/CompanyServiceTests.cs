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
    public class CompanyServiceTests
    {
        private readonly IMapper _mapper;
        private ICompanyService _service;

        public CompanyServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessMappingProfile());
            }));
        }

        [Theory]
        [MemberData(nameof(GetAsyncData))]
        public async Task GetAsync_PassQueryModel_ReturnsValidDtos(
            Collection<CompanyEntity> allEntities,
            CompanyQueryModel queryModel,
            int expectedDtoId)
        {
            //Arrange
            CompanyDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CompanyEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CompanyService(new UnitOfWork(db), _mapper);

                dto = await _service.GetAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedDtoId, dto.Id);
        }

        [Theory]
        [MemberData(nameof(GetListAsyncData))]
        public async Task GetListAsync_PassQueryModel_ReturnsValidDtos(
            Collection<CompanyEntity> allEntities,
            CompanyQueryModel queryModel,
            int expectedCollectionSize)
        {
            //Arrange
            List<CompanyDto> dtos;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CompanyEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Action
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CompanyService(new UnitOfWork(db), _mapper);

                dtos = (List<CompanyDto>)await _service.GetListAsync(queryModel);
            }

            //Assert
            Assert.Equal(expectedCollectionSize, dtos.Count);
        }

        [Theory]
        [MemberData(nameof(RemoveAsyncData))]
        public async Task DeleteAsync_CheckSuccess(
            Collection<CompanyEntity> allEntities,
            int entityIdToDelete,
            CompanyDto expectedDto)
        {
            //Arrange
            CompanyDto dto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CompanyEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CompanyService(new UnitOfWork(db), _mapper);

                dto = await _service.RemoveAsync(entityIdToDelete);
            }

            //Assert
            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.Title, dto.Title);
        }

        [Theory]
        [MemberData(nameof(CreateAsyncData))]
        public async Task CreateAsync_CheckSuccess(
            Collection<CompanyEntity> allEntities,
            CompanyDto expectedDto)
        {
            //Arrange
            CompanyDto createdDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CompanyEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CompanyService(new UnitOfWork(db), _mapper);

                createdDto = await _service.CreateAsync(expectedDto);
            }

            //Assert
            Assert.Equal(expectedDto.Title, createdDto.Title);
            Assert.Equal(expectedDto.YearOfFoundation, createdDto.YearOfFoundation);

        }

        [Theory]
        [MemberData(nameof(ModifyAsyncData))]
        public async Task ModifyAsync_CheckSuccess(
            Collection<CompanyEntity> allEntities,
            CompanyDto entityToUpdate)
        {
            //Arrange
            CompanyDto updatedDto;

            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();

                db.Set<CompanyEntity>().AddRange(allEntities);

                await db.SaveChangesAsync();
            }

            //Act
            await using (var db = new Context(GetContextOptionsBuilder().Options))
            {
                _service = new CompanyService(new UnitOfWork(db), _mapper);

                await _service.UpdateAsync(entityToUpdate);

                var queryModel = new CompanyQueryModel()
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
            var items = new Collection<CompanyEntity>
            {
                new CompanyEntity()
                {
                    Id = 1
                },
                new CompanyEntity()
                {
                    Id = 2
                }
            };

            await using var db = new Context(GetContextOptionsBuilder().Options);
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            await db.Set<CompanyEntity>().AddRangeAsync(items);
            await db.SaveChangesAsync();

            //Act & Assert
            await using var dbToDelete = new Context(GetContextOptionsBuilder().Options);
            _service = new CompanyService(new UnitOfWork(dbToDelete), _mapper);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.RemoveAsync(33));
        }

        public static IEnumerable<object[]> GetListAsyncData()
        {
            var data = new List<object[]>
            {
                new object[]
                {
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            YearOfFoundation = 2000,
                            Title = "Title1"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            YearOfFoundation = 2000,
                            Title = "Title1"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            YearOfFoundation = 2001,
                            Title = "Title2"
                        },
                        new CompanyEntity()
                        {
                            Id = 4,
                            YearOfFoundation = 2001,
                            Title = "NotTitle"
                        },
                    },
                    new CompanyQueryModel
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
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title1",
                            YearOfFoundation = 2001
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title2",
                            YearOfFoundation = 2001
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Empty",
                            YearOfFoundation = 2001
                        }
                    },
                    new CompanyQueryModel()
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
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        }
                    },
                    2,
                    new CompanyDto()
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
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title1"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title2"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Title3"
                        },
                        new CompanyEntity()
                        {
                            Id = 4,
                            Title = "Title4"
                        }
                    },
                    new CompanyDto()
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
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CompanyDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                    }
                },
                new object[]
                {
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CompanyDto()
                    {
                        Id = 10001,
                        Title = "Wow",
                        YearOfFoundation = null
                    }
                },
                new object[]
                {
                    new Collection<CompanyEntity>
                    {
                        new CompanyEntity()
                        {
                            Id = 1,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 2,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 3,
                            Title = "Title"
                        },
                        new CompanyEntity()
                        {
                            Id = 4,
                            Title = "Title"
                        }
                    },
                    new CompanyDto()
                    {
                        Title = "Wow",
                        YearOfFoundation = 2001
                    }
                },
            };

            return data;
        }

        private DbContextOptionsBuilder GetContextOptionsBuilder()
        {
            return new DbContextOptionsBuilder().UseInMemoryDatabase("CompanyServiceTestsDb");
        }
    }
}
