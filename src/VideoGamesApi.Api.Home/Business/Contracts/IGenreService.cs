using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IGenreService : IService<GenreDto, int, GenreQueryModel>
    {
        
    }
}
