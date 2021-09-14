namespace VideoGamesApi.Api.Home.Business.Models
{
    public class GenreDto : Dto<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
