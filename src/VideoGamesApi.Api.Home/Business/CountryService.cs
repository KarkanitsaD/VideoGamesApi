using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class CountryService : Service<CountryEntity, int>, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IBusinessMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetAsync(CountryQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<CountryEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entity = await repository.GetAsync(queryParameters);

            return _mapper.Map<CountryEntity, CountryDto>(entity);
        }

        public async Task<IList<CountryDto>> GetListAsync(CountryQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<CountryEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entities = await repository.GetListAsync(queryParameters);

            return _mapper.Map<IList<CountryEntity>, IList<CountryDto>>(entities);
        }

        public async Task<CountryDto> Modify(CountryDto dto)
        {
            var repository = _unitOfWork.GetRepository<CountryEntity, int>();

            var entity = _mapper.Map<CountryDto, CountryEntity>(dto);

            var entityToReturn = repository.Update(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CountryEntity, CountryDto>(entityToReturn);
        }

        public async Task<CountryDto> CreateAsync(CountryDto dto)
        {
            var repository = _unitOfWork.GetRepository<CountryEntity, int>();

            var entity = _mapper.Map<CountryDto, CountryEntity>(dto);

            var entityToReturn = await repository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CountryEntity, CountryDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<CountryDto> dtos)
        {
            var repository = _unitOfWork.GetRepository<CountryEntity, int>();

            var entities = _mapper.Map<IEnumerable<CountryDto>, IEnumerable<CountryEntity>>(dtos);

            await repository.InsertAsync(entities);

            await _unitOfWork.SaveChangesAsync();
        }

        public Task<CountryDto> RemoveAsync(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineSortExpression(SortRule<CountryEntity, int> sortRule)
        {
            Expression<Func<CountryEntity, string>> expression = country => country.Title;

            sortRule.Expression = expression;
        }

        protected override FilterRule<CountryEntity, int> GetFilterRule(QueryModel model)
        {
            var countryModel = (CountryQueryModel)model;

            var filterRule = new FilterRule<CountryEntity, int>()
            {
                Expression = country =>
                    (countryModel.Id != null && country.Id == countryModel.Id || countryModel.Id == null)
                    && (countryModel.Title != null && country.Title.Contains(countryModel.Title) ||
                        countryModel.Title == null)
            };

            return filterRule;
        }
    }
}
