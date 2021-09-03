using AutoMapper;
using VideoGamesApi.Api.Home.Contracts;

namespace VideoGamesApi.Api.Home.Mapping
{
    public class PresentationMapper : IPresentationMapper
    {
        private readonly IMapper _mapper;

        public PresentationMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
