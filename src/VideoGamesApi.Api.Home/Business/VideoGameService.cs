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
    public class VideoGameService : BaseService<VideoGameEntity, int, VideoGameDto, int, VideoGameQueryModel>, IVideoGameService
    {
        public VideoGameService(IUnitOfWork unitOfWork, IBusinessMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override async Task<VideoGameDto> RemoveAsync(int id)
        {
            var repository = UnitOfWork.GetRepository<VideoGameEntity, int>();

            var queryParameters = new QueryParameters<VideoGameEntity, int>
            {
                FilterRule = new FilterRule<VideoGameEntity, int>
                {
                    Expression = game => game.Id == id
                }
            };

            var entityToDelete = await repository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = repository.Delete(entityToDelete);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<VideoGameEntity, VideoGameDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<VideoGameEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = game => game.Title;
        }

        protected override FilterRule<VideoGameEntity, int> GetFilterRule(VideoGameQueryModel model)
        {
            var gameModel = model;

            var filterRule = new FilterRule<VideoGameEntity, int>
            {
                Expression = game =>
                    (gameModel.Id != null && game.Id == gameModel.Id || gameModel.Id == null)
                    && (gameModel.MinRating != null && game.Rating > gameModel.MinRating || gameModel.MinRating == null)
                    && (gameModel.Title != null && game.Title.Contains(gameModel.Title) || gameModel.Title == null)
            };

            return filterRule;
        }
    }
}
