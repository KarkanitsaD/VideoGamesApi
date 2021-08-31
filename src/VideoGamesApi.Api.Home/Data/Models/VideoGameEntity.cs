using System.Collections.Generic;

namespace VideoGamesApi.Api.Home.Data.Models
{
    public class VideoGameEntity : Entity<int>
    {
        public string Title { get; set; }

        public int? YearOfIssue { get; set; }

        public float? Rating { get; set; }

        public List<CountryEntity> Countries { get; set; }

        public List<CompanyEntity> Companies { get; set; }

        public List<GenreEntity> Genres { get; set; }
    }
}
