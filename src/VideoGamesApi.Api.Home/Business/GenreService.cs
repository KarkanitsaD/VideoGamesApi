using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business
{
    public class GenreService : IGenreService
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
    }
}
