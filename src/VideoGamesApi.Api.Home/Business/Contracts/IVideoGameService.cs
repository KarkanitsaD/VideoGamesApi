using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IVideoGameService
    {
        Task<VideoGameDto> GetAsync(VideoGameQueryModel queryModel);

        Task<IList<VideoGameDto>> GetListAsync(VideoGameQueryModel queryModel);

        Task<VideoGameDto> Modify(VideoGameDto dto);

        Task<VideoGameDto> CreateAsync(VideoGameDto dto);

        Task CreateListAsync(IEnumerable<VideoGameDto> dtos);

        Task<VideoGameDto> RemoveAsync(VideoGameDto dto);
    }
}
