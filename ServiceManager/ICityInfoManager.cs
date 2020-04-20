using CityInfo.API.Entities;
using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API.ServiceManager
{
    public interface ICityInfoManager
    {
        IEnumerable<CityWithoutPointsOfInterestDetail> GetCities();

        City GetCity(int cityId, bool includePointsOfInterest);
    }
}