namespace VideoGamesApi.Api.Home.Business.Models
{
    public class VideoGameDto : Dto<int>
    {
        public string Title { get; set; }

        public int? YearOfIssue { get; set; }

        public float? Rating { get; set; }
    }
}
