using CityInfo.API.Entities;
using CityInfo.API.ServiceManager;
using CityInfo.API.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CityInfo.API.Test
{
    public class CityInfoManagerShould
    {


        [Test]
        public void GetCityShouldReturnList()
        {

            var cityList = new List<City>()
            {
                new City(){Id = 1, Name = "TestCity1", Description = "TestDescription1" },
                new City(){Id = 2, Name = "TestCity2", Description = "TestDescription2" },
                new City(){Id = 1, Name = "TestCity1", Description = "TestDescription1" }
            };

            IEnumerable<City> expected = cityList;

            var mockCityInfoRepository = new Mock<ICityInfoRepository>();


            mockCityInfoRepository.Setup(x => x.GetCities()).Returns(expected);

            var sut = new CityInfoManager(mockCityInfoRepository.Object);

            var cities = sut.GetCities();

            Assert.That(cities, Is.Not.Null);

        }



    }
}
