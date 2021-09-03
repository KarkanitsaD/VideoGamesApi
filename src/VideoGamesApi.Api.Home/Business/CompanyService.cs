using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class CompanyService : Service<CompanyEntity, int>, ICompanyService
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

        protected override void DefineSortExpression(SortRule<CompanyEntity, int> sortRule)
        {
            Expression<Func<CompanyEntity, string>> expression = company => company.Title;

            sortRule.Expression = expression;
        }

        protected override FilterRule<CompanyEntity, int> GetFilterRule(QueryModel model)
        {
            var companyModel = (CompanyQueryModel)model;

            var filterRule = new FilterRule<CompanyEntity, int>
            {
                Expression = company =>
                    (companyModel.Id != null && company.Id == companyModel.Id || companyModel.Id == null)
                    && (companyModel.Title != null && company.Title.Contains(companyModel.Title) || companyModel.Title == null)
            };

            return filterRule;
        }
    }
}
