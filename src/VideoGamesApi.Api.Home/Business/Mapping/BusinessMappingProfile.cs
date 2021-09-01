using AutoMapper;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Business.Mapping
{
    public class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            CreateMap<VideoGameEntity, VideoGameDto>().ReverseMap();

            CreateMap<CountryEntity, CountryDto>().ReverseMap();

            CreateMap<CompanyEntity, CompanyDto>().ReverseMap();

            CreateMap<GenreEntity, GenreDto>().ReverseMap();
        }
    }
}
