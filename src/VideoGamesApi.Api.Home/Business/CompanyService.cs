using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business
{
    public class CompanyService : ICompanyService
    {
        public Task<CompanyDto> GetAsync(CompanyQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> GetListAsync(CompanyQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public CompanyDto Modify(CompanyDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> CreateAsync(CompanyDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> CreateListAsync(IEnumerable<CompanyDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public Task<CompanyDto> RemoveAsync(CompanyDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
