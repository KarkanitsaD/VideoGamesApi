using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data.Repositories
{
    public class CountryRepository : Repository<CountryEntity, int>, ICountryRepository
    {
        public CountryRepository(Context context) : base(context)
        {

        }
    }
}
