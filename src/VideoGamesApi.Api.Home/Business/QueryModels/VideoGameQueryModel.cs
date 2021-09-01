namespace VideoGamesApi.Api.Home.Business.QueryModels
{
    public class VideoGameQueryModel : QueryModel<int>
    {
        public string Title { get; set; }

        public float? Rating { get; set; }
    }
}
