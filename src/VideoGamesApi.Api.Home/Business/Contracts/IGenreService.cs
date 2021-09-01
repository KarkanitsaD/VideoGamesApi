using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IGenreService
    {
        Task<GenreDto> GetAsync(GenreQueryModel queryModel);

        Task<GenreDto> GetListAsync(GenreQueryModel queryModel);

        GenreDto Modify(GenreDto dto);

        Task<GenreDto> CreateAsync(GenreDto dto);

        Task<GenreDto> CreateListAsync(IEnumerable<GenreDto> dtos);

        Task<GenreDto> RemoveAsync(GenreDto dto);
    }
}
