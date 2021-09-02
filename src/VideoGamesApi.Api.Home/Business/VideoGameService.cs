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
    public class VideoGameService : Service<VideoGameEntity, int>, IVideoGameService
    {
        public Task<VideoGameDto> GetAsync(VideoGameQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> GetListAsync(VideoGameQueryModel queryModel)
        {
            throw new System.NotImplementedException();
        }

        public VideoGameDto Modify(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> CreateAsync(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> CreateListAsync(IEnumerable<VideoGameDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public Task<VideoGameDto> RemoveAsync(VideoGameDto dto)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineSortExpression(SortRule<VideoGameEntity, int> sortRule)
        {
            Expression<Func<VideoGameEntity, string>> expression = game => game.Title;

            sortRule.Expression = expression;
        }

        protected override FilterRule<VideoGameEntity, int> GetFilterRule(QueryModel model)
        {
            var gameModel = (VideoGameQueryModel)model;

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
