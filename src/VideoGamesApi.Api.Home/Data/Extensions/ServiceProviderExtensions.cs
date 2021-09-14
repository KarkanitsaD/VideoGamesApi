using Microsoft.Extensions.DependencyInjection;
using VideoGamesApi.Api.Home.Data.Contracts;

namespace VideoGamesApi.Api.Home.Data.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
