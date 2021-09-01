using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business
{
    public class CountryService : ICountryService
    {
        public Task<CountryDto> GetAsync(CountryQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> GetListAsync(CountryQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public CountryDto Modify(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> CreateAsync(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> CreateListAsync(IEnumerable<CountryDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> RemoveAsync(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
