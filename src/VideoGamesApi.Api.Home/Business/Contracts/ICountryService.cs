using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    // this Service looks very similar as all another, pls simplify this copy past. don't forget about implementation
    public interface ICountryService
    {
        Task<CountryDto> GetAsync(CountryQueryModel queryModel);

        Task<IList<CountryDto>> GetListAsync(CountryQueryModel queryModel);

        // bad name, update looks better
        // Why this method is Async but does not have Async postfix in the name?
        Task<CountryDto> Modify(CountryDto dto);

        Task<CountryDto> CreateAsync(CountryDto dto);

        Task CreateListAsync(IEnumerable<CountryDto> dtos);

        Task<CountryDto> RemoveAsync(int id);
    }
}
