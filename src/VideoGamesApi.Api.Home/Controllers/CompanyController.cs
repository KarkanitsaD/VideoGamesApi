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
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<CompanyModel> Get(int id)
        {
            var queryModel = new CompanyQueryModel
            {
                Id = id
            };

            var model = await _service.GetAsync(queryModel);

            return _mapper.Map<CompanyDto, CompanyModel>(model);
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyModel>> Get([FromQuery] CompanyQueryModel queryModel)
        {
            var models = await _service.GetListAsync(queryModel);

            return _mapper.Map<IList<CompanyDto>, IList<CompanyModel>>(models);
        }

        [HttpPost]
        public async Task<CompanyModel> Post([FromBody] CompanyModel model)
        {
            var dto = _mapper.Map<CompanyModel, CompanyDto>(model);

            var dtoToReturn = await _service.CreateAsync(dto);

            return _mapper.Map<CompanyDto, CompanyModel>(dtoToReturn);
        }

        [HttpPost]
        public async Task Post([FromBody] IList<CompanyModel> models)
        {
            var dtos = _mapper.Map<IList<CompanyModel>, IList<CompanyDto>>(models);

            await _service.CreateListAsync(dtos);
        }

        [HttpPut("{id}")]
        public async Task<CompanyModel> Put(int id, [FromBody] CompanyModel model)
        {
            var dto = _mapper.Map<CompanyModel, CompanyDto>(model);

            var dtoToReturn = await _service.UpdateAsync(dto);

            return _mapper.Map<CompanyDto, CompanyModel>(dtoToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<CompanyModel> Delete(int id)
        {
            var dtoToReturn = await _service.RemoveAsync(id);

            return _mapper.Map<CompanyDto, CompanyModel>(dtoToReturn);
        }
    }
}
