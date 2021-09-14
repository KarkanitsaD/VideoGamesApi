using System.Collections.Generic;

namespace VideoGamesApi.Api.Home.Data.Models
{
    public class GenreEntity : Entity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<VideoGameEntity> VideoGames { get; set; }
    }
}
