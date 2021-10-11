using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data.Repositories
{
    public class VideoGameRepository : Repository<VideoGameEntity, int>, IVideoGameRepository
    {
        public VideoGameRepository(Context context) : base(context)
        {

        }
    }
}
