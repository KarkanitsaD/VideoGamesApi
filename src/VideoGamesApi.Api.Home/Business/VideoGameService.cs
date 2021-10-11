using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly IVideoGameRepository _videoGameRepository;


        public VideoGameService(IMapper mapper, Context context, IVideoGameRepository videoGameRepository)
        {
            _mapper = mapper;
            _context = context;
            _videoGameRepository = videoGameRepository;
        }

        public async Task<VideoGameDto> GetAsync(VideoGameQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entity = await _videoGameRepository.GetAsync(queryParameters);

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entity);
        }

        public async Task<IList<VideoGameDto>> GetListAsync(VideoGameQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entities = await _videoGameRepository.GetListAsync(queryParameters);

            return _mapper.Map<IList<VideoGameEntity>, IList<VideoGameDto>>(entities);
        }

        public async Task<VideoGameDto> UpdateAsync(VideoGameDto dto)
        {
            var entity = _mapper.Map<VideoGameDto, VideoGameEntity>(dto);

            var entityToReturn = _videoGameRepository.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entityToReturn);
        }

        public async Task<VideoGameDto> CreateAsync(VideoGameDto dto)
        {
            var entity = _mapper.Map<VideoGameDto, VideoGameEntity>(dto);

            var entityToReturn = await _videoGameRepository.InsertAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<VideoGameDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<VideoGameDto>, IEnumerable<VideoGameEntity>>(dtos);

            await _videoGameRepository.InsertAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<VideoGameDto> RemoveAsync(int id)
        {
            var queryParameters = new QueryParameters<VideoGameEntity, int>
            {
                FilterRule = new FilterRule<VideoGameEntity, int>
                {
                    Expression = game => game.Id == id
                }
            };

            var entityToDelete = await _videoGameRepository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = _videoGameRepository.Delete(entityToDelete);

            await _context.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(deletedEntity);
        }

        private static QueryParameters<VideoGameEntity, int> GetQueryParameters(VideoGameQueryModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{nameof(model)}");

            var queryParameters = new QueryParameters<VideoGameEntity, int>
            {
                FilterRule = GetFilterRule(model),
                SortRule = GetSortRule(model),
                PageRule = GetPageRule(model)
            };

            return queryParameters;
        }

        private static FilterRule<VideoGameEntity, int> GetFilterRule(VideoGameQueryModel model)
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

        private static SortRule<VideoGameEntity, int> GetSortRule(VideoGameQueryModel model)
        {
            var sortRule = new SortRule<VideoGameEntity, int>();

            if (!model.IsValidSortModel)
                return sortRule;

            sortRule.Order = model.SortOrder == QueryModels.SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        private static PageRule GetPageRule(VideoGameQueryModel model)
        {
            var pageRule = new PageRule();

            if (!model.IsValidPageModel)
                return pageRule;

            pageRule.Index = model.Index;
            pageRule.Size = model.Size;

            return pageRule;
        }

        private static void DefineSortExpression(SortRule<VideoGameEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = game => game.Title;
        }
    }
}
