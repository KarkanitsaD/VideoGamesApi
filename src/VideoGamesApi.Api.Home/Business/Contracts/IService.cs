using System.Collections.Generic;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface IService<TDto, TKey, in TQueryModel>
        where TDto : class, IDto<TKey>
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
