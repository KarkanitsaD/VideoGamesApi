using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Query;
using SortOrder = VideoGamesApi.Api.Home.Business.QueryModels.SortOrder;

namespace VideoGamesApi.Api.Home.Business
{
    public abstract class GenericService<TEntity, TEntityKey, TDto, TDtoKey, TQueryModel>
        where TEntity : class, IEntity<TEntityKey>
        where TDto : class, IDto<TDtoKey>
        where TQueryModel : QueryModel
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IBusinessMapper Mapper;

        protected GenericService(IUnitOfWork unitOfWork, IBusinessMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<TDto> GetAsync(TQueryModel queryModel)
        {
            var repository = UnitOfWork.GetRepository<TEntity, TEntityKey>();

            var queryParameters = GetQueryParameters(queryModel);

            var entity = await repository.GetAsync(queryParameters);

            return Mapper.Map<TEntity, TDto>(entity);

        }

        public async Task<IList<TDto>> GetListAsync(TQueryModel queryModel)
        {
            var repository = UnitOfWork.GetRepository<TEntity, TEntityKey>();

            var queryParameters = GetQueryParameters(queryModel);

            var entities = await repository.GetListAsync(queryParameters);

            return Mapper.Map<IList<TEntity>, IList<TDto>>(entities);
        }

        public async Task<TDto> UpdateAsync(TDto dto)
        {
            var repository = UnitOfWork.GetRepository<TEntity, TEntityKey>();

            var entity = Mapper.Map<TDto, TEntity>(dto);

            var entityToReturn = repository.Update(entity);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<TEntity, TDto>(entityToReturn);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var repository = UnitOfWork.GetRepository<TEntity, TEntityKey>();

            var entity = Mapper.Map<TDto, TEntity>(dto);

            var entityToReturn = await repository.InsertAsync(entity);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<TEntity, TDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<TDto> dtos)
        {
            var repository = UnitOfWork.GetRepository<TEntity, TEntityKey>();

            var entities = Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(dtos);

            await repository.InsertAsync(entities);

            await UnitOfWork.SaveChangesAsync();
        }

        public abstract Task<TDto> RemoveAsync(int id);

        private SortRule<TEntity, TEntityKey> GetSortRule(TQueryModel model)
        {
            var sortRule = new SortRule<TEntity, TEntityKey>();

            if (!model.IsValidSortModel)
                return sortRule;

            sortRule.Order = model.SortOrder == SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        protected abstract void DefineSortExpression(SortRule<TEntity, TEntityKey> sortRule);

        private static PageRule GetPageRule(TQueryModel model)
        {
            var pageRule = new PageRule();

            if (!model.IsValidPageModel)
                return pageRule;

            pageRule.Index = model.Index;
            pageRule.Size = model.Size;

            return pageRule;
        }

        protected abstract FilterRule<TEntity, TEntityKey> GetFilterRule(TQueryModel model);

        protected QueryParameters<TEntity, TEntityKey> GetQueryParameters(TQueryModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{nameof(model)}");

            var queryParameters = new QueryParameters<TEntity, TEntityKey>
            {
                FilterRule = GetFilterRule(model),
                SortRule = GetSortRule(model),
                PageRule = GetPageRule(model)
            };

            return queryParameters;
        }
    }
}

