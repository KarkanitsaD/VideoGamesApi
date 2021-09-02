namespace VideoGamesApi.Api.Home.Business.QueryModels
{
    public class VideoGameQueryModel : QueryModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public float? Rating { get; set; }
    }
}
