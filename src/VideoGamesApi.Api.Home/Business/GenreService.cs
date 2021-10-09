using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class GenreService : BaseService<GenreEntity, int, GenreDto, int, GenreQueryModel>, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<GenreDto> RemoveAsync(int id)
        {
            var repository = UnitOfWork.GetRepository<GenreEntity, int>();

            var queryParameters = new QueryParameters<GenreEntity, int>
            {
                FilterRule = new FilterRule<GenreEntity, int>
                {
                    Expression = genre => genre.Id == id
                }
            };

            var entityToDelete = await repository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = repository.Delete(entityToDelete);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<GenreEntity, GenreDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<GenreEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = genre => genre.Title;
        }

        protected override FilterRule<GenreEntity, int> GetFilterRule(GenreQueryModel model)
        {
            var genreModel = model;

            var filterExpression = new FilterRule<GenreEntity, int>
            {
                Expression = genre =>
                    (genreModel.Id != null && genre.Id == genreModel.Id || genreModel.Id == null)
                    && (genreModel.Title != null && genre.Title.Contains(genreModel.Title) || genreModel.Title == null)
            };

            return filterExpression;
        }
    }
}

