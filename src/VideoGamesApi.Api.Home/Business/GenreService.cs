using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class GenreService : Service<GenreEntity, int>, IGenreService
    {
        public Task<GenreDto> GetAsync(GenreQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<GenreDto> GetListAsync(GenreQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public GenreDto Modify(GenreDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<GenreDto> CreateAsync(GenreDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<GenreDto> CreateListAsync(IEnumerable<GenreDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public Task<GenreDto> RemoveAsync(GenreDto dto)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineSortExpression(SortRule<GenreEntity, int> sortRule)
        {
            Expression<Func<GenreEntity, string>> expression = genre => genre.Title;
        }

        protected override FilterRule<GenreEntity, int> GetFilterRule(QueryModel model)
        {
            var genreModel = (GenreQueryModel)model;

            var filterExpression = new FilterRule<GenreEntity, int>()
            {
                Expression = genre =>
                    (genreModel.Id != null && genre.Id == genreModel.Id || genreModel.Id == null)
                    && (genreModel.Title != null && genre.Title.Contains(genreModel.Title) || genreModel.Title == null)
            };

            return filterExpression;
        }
    }
}
