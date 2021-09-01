namespace VideoGamesApi.Api.Home.Business.Models
{
    public class CompanyDto : Dto<int>
    {
        public string Title { get; set; }

        public int? YearOfFoundation { get; set; }

        public int? CountryId { get; set; }
    }
}
