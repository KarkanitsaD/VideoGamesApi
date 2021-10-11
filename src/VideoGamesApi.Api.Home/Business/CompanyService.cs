using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly ICompanyRepository _companyRepository;


        public CompanyService(IMapper mapper, Context context, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _context = context;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> GetAsync(CompanyQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entity = await _companyRepository.GetAsync(queryParameters);

            return _mapper.Map<CompanyEntity, CompanyDto>(entity);
        }

        public async Task<IList<CompanyDto>> GetListAsync(CompanyQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entities = await _companyRepository.GetListAsync(queryParameters);

            return _mapper.Map<IList<CompanyEntity>, IList<CompanyDto>>(entities);
        }

        public async Task<CompanyDto> UpdateAsync(CompanyDto dto)
        {
            var entity = _mapper.Map<CompanyDto, CompanyEntity>(dto);

            var entityToReturn = _companyRepository.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(entityToReturn);
        }

        public async Task<CompanyDto> CreateAsync(CompanyDto dto)
        {
            var entity = _mapper.Map<CompanyDto, CompanyEntity>(dto);

            var entityToReturn = await _companyRepository.InsertAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<CompanyDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<CompanyDto>, IEnumerable<CompanyEntity>>(dtos);

            await _companyRepository.InsertAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<CompanyDto> RemoveAsync(int id)
        {
            var queryParameters = new QueryParameters<CompanyEntity, int>
            {
                FilterRule = new FilterRule<CompanyEntity, int>
                {
                    Expression = company => company.Id == id
                }
            };

            var entityToDelete = await _companyRepository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = _companyRepository.Delete(entityToDelete);

            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(deletedEntity);
        }

        private static QueryParameters<CompanyEntity, int> GetQueryParameters(CompanyQueryModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{nameof(model)}");

            var queryParameters = new QueryParameters<CompanyEntity, int>
            {
                FilterRule = GetFilterRule(model),
                SortRule = GetSortRule(model),
                PageRule = GetPageRule(model)
            };

            return queryParameters;
        }

        private static FilterRule<CompanyEntity, int> GetFilterRule(CompanyQueryModel model)
        {
            var companyModel = model;

            var filterRule = new FilterRule<CompanyEntity, int>
            {
                Expression = company =>
                    (companyModel.Id != null && company.Id == companyModel.Id || companyModel.Id == null)
                    && (companyModel.Title != null && company.Title.Contains(companyModel.Title) || companyModel.Title == null)
            };

            return filterRule;
        }

        private static SortRule<CompanyEntity, int> GetSortRule(CompanyQueryModel model)
        {
            var sortRule = new SortRule<CompanyEntity, int>();

            if (!model.IsValidSortModel)
                return sortRule;

            sortRule.Order = model.SortOrder == QueryModels.SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        private static PageRule GetPageRule(CompanyQueryModel model)
        {
            var pageRule = new PageRule();

            if (!model.IsValidPageModel)
                return pageRule;

            pageRule.Index = model.Index;
            pageRule.Size = model.Size;

            return pageRule;
        }

        private static void DefineSortExpression(SortRule<CompanyEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = company => company.Title;
        }
    }
}
