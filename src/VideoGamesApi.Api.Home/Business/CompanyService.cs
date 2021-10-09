using System;
using System.Collections.Generic;
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
    public class CompanyService : BaseService<CompanyEntity, int, CompanyDto, int, CompanyQueryModel>, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<CompanyDto> RemoveAsync(int id)
        {
            var repository = UnitOfWork.GetRepository<CompanyEntity, int>();

            var queryParameters = new QueryParameters<CompanyEntity, int>
            {
                FilterRule = new FilterRule<CompanyEntity, int>
                {
                    Expression = company => company.Id == id
                }
            };

            var entityToDelete = await repository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = repository.Delete(entityToDelete);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<CompanyEntity, CompanyDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<CompanyEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = company => company.Title;
        }

        protected override FilterRule<CompanyEntity, int> GetFilterRule(CompanyQueryModel model)
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
    }
}
