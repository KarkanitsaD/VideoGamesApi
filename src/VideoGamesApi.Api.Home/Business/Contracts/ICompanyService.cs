using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetAsync(CompanyQueryModel queryModel);

        Task<CompanyDto> GetListAsync(CompanyQueryModel queryModel);

        CompanyDto Modify(CompanyDto dto);

        Task<CompanyDto> CreateAsync(CompanyDto dto);

        Task<CompanyDto> CreateListAsync(IEnumerable<CompanyDto> dtos);

        Task<CompanyDto> RemoveAsync(CompanyDto dto);
    }
}
