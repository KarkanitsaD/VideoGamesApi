using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoGamesApi.Api.Home.Business.Contracts;
using AutoMapper;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;
using VideoGamesApi.Api.Home.Models;

namespace VideoGamesApi.Api.Home.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;
        private readonly IMapper _mapper;

        public CountryController(ICountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<CountryModel> Get(int id)
        {
            var queryModel = new CountryQueryModel
            {
                Id = id
            };

            var model = await _service.GetAsync(queryModel);

            return _mapper.Map<CountryDto, CountryModel>(model);
        }

        [HttpGet]
        public async Task<IEnumerable<CountryModel>> Get([FromQuery] CountryQueryModel queryModel)
        {
            var models = await _service.GetListAsync(queryModel);

            return _mapper.Map<IList<CountryDto>, IList<CountryModel>>(models);
        }

        [HttpPost]
        public async Task<CountryModel> Post([FromBody] CountryModel model)
        {
            var dto = _mapper.Map<CountryModel, CountryDto>(model);

            var dtoToReturn = await _service.CreateAsync(dto);

            return _mapper.Map<CountryDto, CountryModel>(dtoToReturn);
        }

        [HttpPost]
        public async Task Post([FromBody] IList<CountryModel> models)
        {
            var dtos = _mapper.Map<IList<CountryModel>, IList<CountryDto>>(models);

            await _service.CreateListAsync(dtos);
        }

        [HttpPut("{id}")]
        public async Task<CountryModel> Put(int id, [FromBody] CountryModel model)
        {
            var dto = _mapper.Map<CountryModel, CountryDto>(model);

            var dtoToReturn = await _service.UpdateAsync(dto);

            return _mapper.Map<CountryDto, CountryModel>(dtoToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<CountryModel> Delete(int id)
        {
            var dtoToReturn = await _service.RemoveAsync(id);

            return _mapper.Map<CountryDto, CountryModel>(dtoToReturn);
        }
    }
}
