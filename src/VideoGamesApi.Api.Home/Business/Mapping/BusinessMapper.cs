using AutoMapper;
using VideoGamesApi.Api.Home.Business.Contracts;

namespace VideoGamesApi.Api.Home.Business.Mapping
{
    public class BusinessMapper : IBusinessMapper
    {
        private readonly IMapper _mapper;

        public BusinessMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
