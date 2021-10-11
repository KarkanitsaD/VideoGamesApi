using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data.Repositories
{
    public class GenreRepository : Repository<GenreEntity, int>, IGenreRepository
    {
        public GenreRepository(Context context) : base(context)
        {

        }
    }
}
