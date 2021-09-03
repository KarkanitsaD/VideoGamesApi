using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class CountryService : Service<CountryEntity, int>, ICountryService
    {
        public Task<CountryDto> GetAsync(CountryQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> GetListAsync(CountryQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public CountryDto Modify(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> CreateAsync(CountryDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<CountryDto> CreateListAsync(IEnumerable<CountryDto> dtos)
        {
            throw new System.NotImplementedException();
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
