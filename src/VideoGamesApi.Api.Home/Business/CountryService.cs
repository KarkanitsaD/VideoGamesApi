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
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly ICountryRepository _countryRepository;
        private readonly ICompanyRepository _companyRepository;


        public CountryService(IMapper mapper, Context context, ICountryRepository countryRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _context = context;
            _countryRepository = countryRepository;
            _companyRepository = companyRepository;
        }

        public async Task<CountryDto> GetAsync(CountryQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entity = await _countryRepository.GetAsync(queryParameters);

            return _mapper.Map<CountryEntity, CountryDto>(entity);
        }

        public async Task<IList<CountryDto>> GetListAsync(CountryQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entities = await _countryRepository.GetListAsync(queryParameters);

            return _mapper.Map<IList<CountryEntity>, IList<CountryDto>>(entities);
        }

        public async Task<CountryDto> UpdateAsync(CountryDto dto)
        {
            var entity = _mapper.Map<CountryDto, CountryEntity>(dto);

            var entityToReturn = _countryRepository.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryEntity, CountryDto>(entityToReturn);
        }

        public async Task<CountryDto> CreateAsync(CountryDto dto)
        {
            var entity = _mapper.Map<CountryDto, CountryEntity>(dto);

            var entityToReturn = await _countryRepository.InsertAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryEntity, CountryDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<CountryDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<CountryDto>, IEnumerable<CountryEntity>>(dtos);

            await _countryRepository.InsertAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<CountryDto> RemoveAsync(int id)
        {
            var queryParameters = new QueryParameters<CountryEntity, int>
            {
                FilterRule = new FilterRule<CountryEntity, int>
                {
                    Expression = country => country.Id == id
                }
            };

            var entityToDelete = await _countryRepository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var companyQueryParameters = new QueryParameters<CompanyEntity, int>
            {
                FilterRule = new FilterRule<CompanyEntity, int>
                {
                    Expression = company => company.CountryId == id
                }
            };

            var companiesToModify =
                await _companyRepository.GetListAsync(companyQueryParameters);

            foreach (var company in companiesToModify)
            {
                company.CountryId = null;
            }

            var deletedEntity = _countryRepository.Delete(entityToDelete);

            await _context.SaveChangesAsync();

            return _mapper.Map<CountryEntity, CountryDto>(deletedEntity);
        }

        private static QueryParameters<CountryEntity, int> GetQueryParameters(CountryQueryModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{nameof(model)}");

            var queryParameters = new QueryParameters<CountryEntity, int>
            {
                FilterRule = GetFilterRule(model),
                SortRule = GetSortRule(model),
                PageRule = GetPageRule(model)
            };

            return queryParameters;
        }

        private static FilterRule<CountryEntity, int> GetFilterRule(CountryQueryModel model)
        {
            var countryModel = model;

            var filterRule = new FilterRule<CountryEntity, int>
            {
                Expression = country =>
                    (countryModel.Id != null && country.Id == countryModel.Id || countryModel.Id == null)
                    && (countryModel.Title != null && country.Title.Contains(countryModel.Title) ||
                        countryModel.Title == null)
            };

            return filterRule;
        }

        private static SortRule<CountryEntity, int> GetSortRule(CountryQueryModel model)
        {
            var sortRule = new SortRule<CountryEntity, int>();

            if (!model.IsValidSortModel)
                return sortRule;

            sortRule.Order = model.SortOrder == QueryModels.SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        private static PageRule GetPageRule(CountryQueryModel model)
        {
            var pageRule = new PageRule();

            if (!model.IsValidPageModel)
                return pageRule;

            pageRule.Index = model.Index;
            pageRule.Size = model.Size;

            return pageRule;
        }

        private static void DefineSortExpression(SortRule<CountryEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = country => country.Title;
        }
    }
}
