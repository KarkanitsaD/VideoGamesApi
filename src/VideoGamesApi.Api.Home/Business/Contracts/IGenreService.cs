using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IGenreService
    {
        Task<GenreDto> GetAsync(GenreQueryModel queryModel);

        Task<IList<GenreDto>> GetListAsync(GenreQueryModel queryModel);

        Task<GenreDto> Modify(GenreDto dto);

        Task<GenreDto> CreateAsync(GenreDto dto);

        Task CreateListAsync(IEnumerable<GenreDto> dtos);

        Task<GenreDto> RemoveAsync(int id);
    }
}
