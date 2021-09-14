using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICountryService : IBaseService<CountryEntity, int, CountryDto, int, CountryQueryModel>
    {

    }
}
