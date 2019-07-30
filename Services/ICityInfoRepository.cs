﻿using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    interface ICityInfoRepository
    {

        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointsOfInterest);

        IEnumerable<PointOfInterest> GetPointOfInterestsForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
    }
}
