using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business
{
    public class VideoGameService : IVideoGameService
    {
        public Task<VideoGameDto> GetAsync(VideoGameQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> GetListAsync(VideoGameQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public VideoGameDto Modify(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> CreateAsync(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> CreateListAsync(IEnumerable<VideoGameDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> RemoveAsync(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
