using Microsoft.Extensions.DependencyInjection;
using VideoGamesApi.Api.Home.Business.Contracts;

namespace VideoGamesApi.Api.Home.Business.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IVideoGameService, VideoGameService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICountryService, CountryService>();

            return services;
        }
    }
}
