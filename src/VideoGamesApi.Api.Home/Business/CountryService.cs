using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class CountryService : BaseService<CountryEntity, int, CountryDto, int, CountryQueryModel>, ICountryService
    {
        public CountryService(IUnitOfWork unitOfWork, IBusinessMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<CountryDto> RemoveAsync(int id)
        {
            var repository = UnitOfWork.GetRepository<CountryEntity, int>();

            var queryParameters = new QueryParameters<CountryEntity, int>
            {
                FilterRule = new FilterRule<CountryEntity, int>
                {
                    Expression = country => country.Id == id
                }
            };

            var entityToDelete = await repository.GetAsync(queryParameters);

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
                await UnitOfWork.GetRepository<CompanyEntity, int>().GetListAsync(companyQueryParameters);

            foreach (var company in companiesToModify)
            {
                company.CountryId = null;
            }

            var deletedEntity = repository.Delete(entityToDelete);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<CountryEntity, CountryDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<CountryEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = country => country.Title;
        }

        protected override FilterRule<CountryEntity, int> GetFilterRule(CountryQueryModel model)
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
    }
}
