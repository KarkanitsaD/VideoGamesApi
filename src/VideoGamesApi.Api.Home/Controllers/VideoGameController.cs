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
    [Route("api/videoGames")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameService _service;
        private readonly IMapper _mapper;

        public VideoGameController(IVideoGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<VideoGameModel> Get(int id)
        {
            var queryModel = new VideoGameQueryModel
            {
                Id = id
            };

            var model = await _service.GetAsync(queryModel);

            return _mapper.Map<VideoGameDto, VideoGameModel>(model);
        }

        [HttpGet]
        public async Task<IEnumerable<VideoGameModel>> Get([FromQuery] VideoGameQueryModel queryModel)
        {
            var models = await _service.GetListAsync(queryModel);

            return _mapper.Map<IList<VideoGameDto>, IList<VideoGameModel>>(models);
        }

        [HttpPost]
        public async Task<VideoGameModel> Post([FromBody] VideoGameModel model)
        {
            var dto = _mapper.Map<VideoGameModel, VideoGameDto>(model);

            var dtoToReturn = await _service.CreateAsync(dto);

            return _mapper.Map<VideoGameDto, VideoGameModel>(dtoToReturn);
        }

        [HttpPost]
        public async Task Post([FromBody] IList<VideoGameModel> models)
        {
            var dtos = _mapper.Map<IList<VideoGameModel>, IList<VideoGameDto>>(models);

            await _service.CreateListAsync(dtos);
        }

        [HttpPut("{id}")]
        public async Task<VideoGameModel> Put(int id, [FromQuery] VideoGameModel model)
        {
            var dto = _mapper.Map<VideoGameModel, VideoGameDto>(model);

            var dtoToReturn = await _service.Modify(dto);

            return _mapper.Map<VideoGameDto, VideoGameModel>(dtoToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<VideoGameModel> Delete(int id)
        {
            var dtoToReturn = await _service.RemoveAsync(id);

            return _mapper.Map<VideoGameDto, VideoGameModel>(dtoToReturn);
        }
    }
}
