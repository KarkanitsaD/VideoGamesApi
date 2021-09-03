using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICountryService
    {
        Task<CountryDto> GetAsync(CountryQueryModel queryModel);

        Task<IList<CountryDto>> GetListAsync(CountryQueryModel queryModel);

        Task<CountryDto> Modify(CountryDto dto);

        Task<CountryDto> CreateAsync(CountryDto dto);

        Task CreateListAsync(IEnumerable<CountryDto> dtos);

        Task<CountryDto> RemoveAsync(int id);
    }
}
