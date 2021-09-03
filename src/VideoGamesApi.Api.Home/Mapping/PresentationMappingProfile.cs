using AutoMapper;
using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Models;

namespace VideoGamesApi.Api.Home.Mapping
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<CompanyModel, CompanyDto>().ReverseMap();

            CreateMap<CountryModel, CountryDto>().ReverseMap();

            CreateMap<GenreModel, GenreDto>().ReverseMap();

            CreateMap<VideoGameModel, VideoGameDto>().ReverseMap();
        }
    }
}
