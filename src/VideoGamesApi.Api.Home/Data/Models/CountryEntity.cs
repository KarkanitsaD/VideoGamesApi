using System.Collections.Generic;

namespace VideoGamesApi.Api.Home.Data.Models
{
    public class CountryEntity : Entity<int>
    {
        public string Title { get; set; }

        public List<CompanyEntity> Companies { get; set; }

        public List<VideoGameEntity> VideoGames { get; set; }
    }
}
