using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Business
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<CompanyDto> GetAsync(CompanyQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();
            return null;

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
