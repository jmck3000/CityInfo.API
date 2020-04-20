using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using System.Collections.Generic;

namespace CityInfo.API.ServiceManager
{
    public class CityInfoManager : ICityInfoManager
    {

        private ICityInfoRepository _cityInfoRepository;

        public CityInfoManager(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }


        public IEnumerable<CityWithoutPointsOfInterestDetail> GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDetail>>(cityEntities);

            return results;
        }


    }
}
