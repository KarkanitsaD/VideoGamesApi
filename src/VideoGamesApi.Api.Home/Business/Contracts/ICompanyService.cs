using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetAsync(CompanyQueryModel queryModel);

        Task<IList<CompanyDto>> GetListAsync(CompanyQueryModel queryModel);

        Task<CompanyDto> UpdateAsync(CompanyDto dto);

        Task<CompanyDto> CreateAsync(CompanyDto dto);

        Task CreateListAsync(IEnumerable<CompanyDto> dtos);

        Task<CompanyDto> RemoveAsync(int id);
    }
}
