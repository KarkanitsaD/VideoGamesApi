using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Models;

namespace VideoGamesApi.Api.Home.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;
        private readonly IMapper _mapper;

        public GenreController(IGenreService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<GenreModel> Get(int id)
        {
            var queryModel = new GenreQueryModel
            {
                Id = id
            };

            var model = await _service.GetAsync(queryModel);

            return _mapper.Map<GenreDto, GenreModel>(model);
        }

        [HttpGet]
        public async Task<IEnumerable<GenreModel>> Get([FromQuery] GenreQueryModel queryModel)
        {
            var models = await _service.GetListAsync(queryModel);

            return _mapper.Map<IList<GenreDto>, IList<GenreModel>>(models);
        }

        [HttpPost]
        public async Task<GenreModel> Post([FromBody] GenreModel model)
        {
            var dto = _mapper.Map<GenreModel, GenreDto>(model);

            var dtoToReturn = await _service.CreateAsync(dto);

            return _mapper.Map<GenreDto, GenreModel>(dtoToReturn);
        }

        [HttpPost]
        public async Task Post([FromBody] IList<GenreModel> models)
        {
            var dtos = _mapper.Map<IList<GenreModel>, IList<GenreDto>>(models);

            await _service.CreateListAsync(dtos);
        }

        [HttpPut("{id}")]
        public async Task<GenreModel> Put(int id, [FromBody] GenreModel model)
        {
            var dto = _mapper.Map<GenreModel, GenreDto>(model);

            var dtoToReturn = await _service.Modify(dto);

            return _mapper.Map<GenreDto, GenreModel>(dtoToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<GenreModel> Delete(int id)
        {
            var dtoToReturn = await _service.RemoveAsync(id);

            return _mapper.Map<GenreDto, GenreModel>(dtoToReturn);
        }
    }
}
