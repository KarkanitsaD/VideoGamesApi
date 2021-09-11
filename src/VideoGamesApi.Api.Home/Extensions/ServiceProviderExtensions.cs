using Microsoft.Extensions.DependencyInjection;
using VideoGamesApi.Api.Home.Contracts;
using VideoGamesApi.Api.Home.Mapping;

namespace VideoGamesApi.Api.Home.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddPresentationMapper(this IServiceCollection services)
        {
            services.AddSingleton<IPresentationMapper, PresentationMapper>();

            return services;
        }
    }
}
