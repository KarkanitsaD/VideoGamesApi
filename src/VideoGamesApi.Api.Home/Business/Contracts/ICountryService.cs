using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICountryService
    {
        Task<CountryDto> GetAsync(CountryQueryModel queryModel);

        Task<CountryDto> GetListAsync(CountryQueryModel queryModel);

        CountryDto Modify(CountryDto dto);

        Task<CountryDto> CreateAsync(CountryDto dto);

        Task<CountryDto> CreateListAsync(IEnumerable<CountryDto> dtos);

        Task<CountryDto> RemoveAsync(CountryDto dto);
    }
}
