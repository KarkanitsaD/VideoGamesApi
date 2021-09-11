﻿using System;
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
    public class VideoGameService : Service<VideoGameEntity, int>, IVideoGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessMapper _mapper;

        public VideoGameService(IUnitOfWork unitOfWork, IBusinessMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VideoGameDto> GetAsync(VideoGameQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entity = await repository.GetAsync(queryParameters);

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entity);
        }

        public async Task<IList<VideoGameDto>> GetListAsync(VideoGameQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entities = await repository.GetListAsync(queryParameters);

            return _mapper.Map<IList<VideoGameEntity>, IList<VideoGameDto>>(entities);
        }

        public async Task<VideoGameDto> UpdateAsync(VideoGameDto dto)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

            var entity = _mapper.Map<VideoGameDto, VideoGameEntity>(dto);

            var entityToReturn = repository.Update(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entityToReturn);
        }

        public async Task<VideoGameDto> CreateAsync(VideoGameDto dto)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

            var entity = _mapper.Map<VideoGameDto, VideoGameEntity>(dto);

            var entityToReturn = await repository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<VideoGameDto> dtos)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

            var entities = _mapper.Map<IEnumerable<VideoGameDto>, IEnumerable<VideoGameEntity>>(dtos);

            await repository.InsertAsync(entities);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<VideoGameDto> RemoveAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<VideoGameEntity, int>();

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

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VideoGameEntity, VideoGameDto>(deletedEntity);
        }

        protected override void DefineSortExpression(SortRule<VideoGameEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = game => game.Title;
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
