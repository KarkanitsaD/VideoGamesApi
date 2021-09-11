﻿using VideoGamesApi.Api.Home.Business.Models;
using VideoGamesApi.Api.Home.Business.QueryModels;

namespace VideoGamesApi.Api.Home.Business.Contracts
{
    public interface ICountryService : IService<CountryDto, int, CountryQueryModel>
    {

    }
}
