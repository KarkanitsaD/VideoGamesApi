using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;


namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IBaseService<TEntity, TEntityKey, TDto, TDtoKey, in TQueryModel>
        where TEntity : class, IEntity<TEntityKey>
        where TDto : class, IDto<TDtoKey>
        where TQueryModel : QueryModel
    {
        Task<TDto> GetAsync(TQueryModel queryModel);

        Task<IList<TDto>> GetListAsync(TQueryModel queryModel);

        Task<TDto> UpdateAsync(TDto dto);

        Task<TDto> CreateAsync(TDto dto);

        Task CreateListAsync(IEnumerable<TDto> dtos);

        Task<TDto> RemoveAsync(int id);
    }
}
