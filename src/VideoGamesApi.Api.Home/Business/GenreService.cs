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
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly IGenreRepository _genreRepository;


        public GenreService(IMapper mapper, Context context, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _context = context;
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> GetAsync(GenreQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entity = await _genreRepository.GetAsync(queryParameters);

            return _mapper.Map<GenreEntity, GenreDto>(entity);
        }

        public async Task<IList<GenreDto>> GetListAsync(GenreQueryModel queryModel)
        {
            var queryParameters = GetQueryParameters(queryModel);

            var entities = await _genreRepository.GetListAsync(queryParameters);

            return _mapper.Map<IList<GenreEntity>, IList<GenreDto>>(entities);
        }

        public async Task<GenreDto> UpdateAsync(GenreDto dto)
        {
            var entity = _mapper.Map<GenreDto, GenreEntity>(dto);

            var entityToReturn = _genreRepository.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<GenreEntity, GenreDto>(entityToReturn);
        }

        public async Task<GenreDto> CreateAsync(GenreDto dto)
        {
            var entity = _mapper.Map<GenreDto, GenreEntity>(dto);

            var entityToReturn = await _genreRepository.InsertAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<GenreEntity, GenreDto>(entityToReturn);
        }

        public async Task CreateListAsync(IEnumerable<GenreDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<GenreDto>, IEnumerable<GenreEntity>>(dtos);

            await _genreRepository.InsertAsync(entities);

            await _context.SaveChangesAsync();
        }

        public async Task<GenreDto> RemoveAsync(int id)
        {
            var queryParameters = new QueryParameters<GenreEntity, int>
            {
                FilterRule = new FilterRule<GenreEntity, int>
                {
                    Expression = genre => genre.Id == id
                }
            };

            var entityToDelete = await _genreRepository.GetAsync(queryParameters);

            if (entityToDelete == null)
                throw new KeyNotFoundException();

            var deletedEntity = _genreRepository.Delete(entityToDelete);

            await _context.SaveChangesAsync();

            return _mapper.Map<GenreEntity, GenreDto>(deletedEntity);
        }

        private static QueryParameters<GenreEntity, int> GetQueryParameters(GenreQueryModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{nameof(model)}");

            var queryParameters = new QueryParameters<GenreEntity, int>
            {
                FilterRule = GetFilterRule(model),
                SortRule = GetSortRule(model),
                PageRule = GetPageRule(model)
            };

            return queryParameters;
        }

        private static FilterRule<GenreEntity, int> GetFilterRule(GenreQueryModel model)
        {
            var genreModel = model;

            var filterExpression = new FilterRule<GenreEntity, int>
            {
                Expression = genre =>
                    (genreModel.Id != null && genre.Id == genreModel.Id || genreModel.Id == null)
                    && (genreModel.Title != null && genre.Title.Contains(genreModel.Title) || genreModel.Title == null)
            };

            return filterExpression;
        }

        private static SortRule<GenreEntity, int> GetSortRule(GenreQueryModel model)
        {
            var sortRule = new SortRule<GenreEntity, int>();

            if (!model.IsValidSortModel)
                return sortRule;

            sortRule.Order = model.SortOrder == QueryModels.SortOrder.Ascending
                ? Data.Query.SortOrder.Ascending
                : Data.Query.SortOrder.Descending;
            DefineSortExpression(sortRule);

            return sortRule;
        }

        private static PageRule GetPageRule(GenreQueryModel model)
        {
            var pageRule = new PageRule();

            if (!model.IsValidPageModel)
                return pageRule;

            pageRule.Index = model.Index;
            pageRule.Size = model.Size;

            return pageRule;
        }

        private static void DefineSortExpression(SortRule<GenreEntity, int> sortRule)
        {
            if (sortRule == null)
                throw new ArgumentNullException(nameof(sortRule));

            sortRule.Expression = genre => genre.Title;
        }
    }
}

