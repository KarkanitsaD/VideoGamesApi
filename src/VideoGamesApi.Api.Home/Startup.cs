using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VideoGamesApi.Api.Home.Business;
using VideoGamesApi.Api.Home.Business.Contracts;
using VideoGamesApi.Api.Home.Business.Mapping;
using VideoGamesApi.Api.Home.Contracts;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Mapping;
using Microsoft.Extensions.Configuration;

namespace VideoGamesApi.Api.Home
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BusinessMappingProfile());
                mc.AddProfile(new PresentationMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            // replace your mapper setup with this one 
            // https://www.codementor.io/@zedotech/how-to-using-automapper-on-asp-net-core-3-0-via-dependencyinjection-zq497lzsq

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));
            services.AddSingleton<IBusinessMapper, BusinessMapper>(); // ???
            services.AddSingleton<IPresentationMapper, PresentationMapper>(); // ???
            services.AddScoped<DbContext, Context>(); // ?????
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IVideoGameService, VideoGameService>(); // why it is AddScoped for usual service?
            services.AddTransient<IGenreService, GenreService>();

            // migrate services configuration like ` services.AddTransient<IGenreService, GenreService>(); ` to business layer
            // migrate UnitOfWork configuration to data layer
            // try to keep Startup.cs file clean
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
