using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data
{
    public class Context : DbContext
    {
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<VideoGameEntity> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VideoGamesApi;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
