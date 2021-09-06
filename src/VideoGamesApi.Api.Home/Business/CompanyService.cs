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
    public class CompanyService : Service<CompanyEntity, int>, ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IBusinessMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyDto> GetAsync(CompanyQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entity = await repository.GetAsync(queryParameters);

            return _mapper.Map<CompanyEntity, CompanyDto>(entity);

        }

        public async Task<IList<CompanyDto>> GetListAsync(CompanyQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entities = await repository.GetListAsync(queryParameters);

            return _mapper.Map<IList<CompanyEntity>, IList<CompanyDto>>(entities);
        }

        public async Task<CompanyDto> Modify(CompanyDto dto)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

            var entity = _mapper.Map<CompanyDto, CompanyEntity>(dto);

            var entityToReturn = repository.Update(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(entityToReturn);
        }

        public async Task<CompanyDto> CreateAsync(CompanyDto dto)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

            var entity = _mapper.Map<CompanyDto, CompanyEntity>(dto);

            var entityToReturn = await repository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<CompanyDto> dtos)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

            var entities = _mapper.Map<IEnumerable<CompanyDto>, IEnumerable<CompanyEntity>>(dtos);

            await repository.InsertAsync(entities);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CompanyDto> RemoveAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<CompanyEntity, int>();

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

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CompanyEntity, CompanyDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<CompanyEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = company => company.Title;
        }

        protected override FilterRule<CompanyEntity, int> GetFilterRule(QueryModel model)
        {
            var companyModel = (CompanyQueryModel)model;

            var filterRule = new FilterRule<CompanyEntity, int>
            {
                Expression = company =>
                    (companyModel.Id != null && company.Id == companyModel.Id || companyModel.Id == null)
                    && (companyModel.Title != null && company.Title.Contains(companyModel.Title, StringComparison.Ordinal) || companyModel.Title == null)
            };

            return filterRule;
        }
    }
}
