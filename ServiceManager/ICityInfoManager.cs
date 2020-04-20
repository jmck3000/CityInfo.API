using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API.ServiceManager
{
    public interface ICityInfoManager
    {
        IEnumerable<CityWithoutPointsOfInterestDetail> GetCities();
    }
}