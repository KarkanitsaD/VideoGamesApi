using System.Collections.Generic;

namespace VideoGamesApi.Api.Home.Data.Models
{
    public class CompanyEntity : Entity<int>
    {
        public string Title { get; set; }

        public int? YearOfFoundation { get; set; }

        public int? CountryId { get; set; }

        public CountryEntity Country { get; set; }

        public List<VideoGameEntity> VideoGames { get; set; }
    }
}
