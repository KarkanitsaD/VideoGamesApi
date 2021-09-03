namespace VideoGamesApi.Api.Home.Models
{
    public class GenreModel : Model<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
