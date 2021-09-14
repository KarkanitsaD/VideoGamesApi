using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data;
using VideoGamesApi.Api.Home.Tests.Data.Fakes;

namespace VideoGamesApi.Api.Home.IntegrationTests.Fakes
{
    public class FakeDbContext : Context
    {
        public FakeDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FakeEntity<int>>();
        }
    }
}
