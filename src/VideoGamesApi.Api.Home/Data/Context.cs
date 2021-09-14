using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Models;
namespace VideoGamesApi.Api.Home.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {

        }

        public Context()
        {

        }

        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<VideoGameEntity> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
