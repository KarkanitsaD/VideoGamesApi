﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;
using VideoGamesApi.Api.Home.Data.Query;

namespace VideoGamesApi.Api.Home.Business
{
    public class GenreService : Service<GenreEntity, int>, IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IBusinessMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreDto> GetAsync(GenreQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<GenreEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entity = await repository.GetAsync(queryParameters);

            return _mapper.Map<GenreEntity, GenreDto>(entity);
        }

        public async Task<IList<GenreDto>> GetListAsync(GenreQueryModel queryModel)
        {
            var repository = _unitOfWork.GetRepository<GenreEntity, int>();

            var queryParameters = GetQueryParameters(queryModel);

            var entities = await repository.GetListAsync(queryParameters);

            return _mapper.Map<IList<GenreEntity>, IList<GenreDto>>(entities);
        }

        public async Task<GenreDto> Modify(GenreDto dto)
        {
            var repository = _unitOfWork.GetRepository<GenreEntity, int>();

            var entity = _mapper.Map<GenreDto, GenreEntity>(dto);

            var entityToReturn = repository.Update(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreEntity, GenreDto>(entityToReturn);
        }

        public async Task<GenreDto> CreateAsync(GenreDto dto)
        {
            var repository = _unitOfWork.GetRepository<GenreEntity, int>();

            var entity = _mapper.Map<GenreDto, GenreEntity>(dto);

            var entityToReturn = await repository.InsertAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GenreEntity, GenreDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<GenreDto> dtos)
        {
            var repository = _unitOfWork.GetRepository<GenreEntity, int>();

            var entities = _mapper.Map<IEnumerable<GenreDto>, IEnumerable<GenreEntity>>(dtos);

            await repository.InsertAsync(entities);

            await _unitOfWork.SaveChangesAsync();
        }

        public Task<GenreDto> RemoveAsync(GenreDto dto)
        {
            throw new System.NotImplementedException();
        }

        protected override void DefineSortExpression(SortRule<GenreEntity, int> sortRule)
        {
            Expression<Func<GenreEntity, string>> expression = genre => genre.Title;
        }

        protected override FilterRule<GenreEntity, int> GetFilterRule(QueryModel model)
        {
            var genreModel = (GenreQueryModel)model;

            var filterExpression = new FilterRule<GenreEntity, int>()
            {
                Expression = genre =>
                    (genreModel.Id != null && genre.Id == genreModel.Id || genreModel.Id == null)
                    && (genreModel.Title != null && genre.Title.Contains(genreModel.Title) || genreModel.Title == null)
            };

            return filterExpression;
        }
    }
}
