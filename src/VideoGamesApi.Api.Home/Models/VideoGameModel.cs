namespace VideoGamesApi.Api.Home.Models
{
    public class VideoGameModel : Model<int>
    {
        public string Title { get; set; }

        public int? YearOfIssue { get; set; }

        public float? Rating { get; set; }
    }
}
