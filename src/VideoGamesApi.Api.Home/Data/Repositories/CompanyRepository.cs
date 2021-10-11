using Microsoft.EntityFrameworkCore;
using VideoGamesApi.Api.Home.Data.Contracts;
using VideoGamesApi.Api.Home.Data.Models;

namespace VideoGamesApi.Api.Home.Data.Repositories
{
    public class CompanyRepository : Repository<CompanyEntity, int>, ICompanyRepository
    {
        public CompanyRepository(Context context) : base(context)
        {

        }
    }
}
