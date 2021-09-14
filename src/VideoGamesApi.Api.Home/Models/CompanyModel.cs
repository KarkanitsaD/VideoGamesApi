namespace VideoGamesApi.Api.Home.Models
{
    public class CompanyModel : Model<int>
    {
        public string Title { get; set; }

        public int? YearOfFoundation { get; set; }

        public int? CountryId { get; set; }
    }
}
