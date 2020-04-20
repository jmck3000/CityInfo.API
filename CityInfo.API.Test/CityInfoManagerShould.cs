using CityInfo.API.Entities;
using CityInfo.API.Mapping;
using CityInfo.API.ServiceManager;
using CityInfo.API.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Test
{
    public class CityInfoManagerShould
    {
        private List<City> _cityList;
        private City _city;
        private List<PointOfInterest> _pointOfInterest


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            _cityList = new List<City>()
            {
                new City(){Id = 1, Name = "TestCity1", Description = "TestDescription1" },
                new City(){Id = 2, Name = "TestCity2", Description = "TestDescription2" },
                new City(){Id = 3, Name = "TestCity3", Description = "TestDescription3" }
            };

            _city = new City() { Id = 1, Name = "TestCity1", Description = "TestDescription1" };

            _pointOfInterest = new List<PointOfInterest>()
            {
                new PointOfInterest() {Id = 1, CityId = 1, City = _city, Name = "Point1", Description = "Desc1"}
            };

        }

        [Test]
        public void GetCitiesShouldReturnItems()
        {
            var mockCityInfoRepository = new Mock<ICityInfoRepository>();

            IEnumerable<City> expected = _cityList;
            mockCityInfoRepository.Setup(x => x.GetCities()).Returns(expected);

            var sut = new CityInfoManager(mockCityInfoRepository.Object);

            var cities = sut.GetCities();

            Assert.That(cities.Count(), Is.EqualTo(3));
        }


        [Test]
        public void CityShouldMatchOnName()
        {
            var mockCityInfoRepository = new Mock<ICityInfoRepository>();
            mockCityInfoRepository.Setup(x => x.GetCity(1, false)).Returns(_city);

            var sut = new CityInfoManager(mockCityInfoRepository.Object);

            var city = sut.GetCity(1, false);

            Assert.That(city.Name, Is.EqualTo("TestCity1"));
        }

        [Test]
        public void CityShouldHavePointOfIntrest()
        {
            var mockCityInfoRepository = new Mock<ICityInfoRepository>();
            mockCityInfoRepository.Setup(x => x.GetCity(1, false)).Returns(_city);

            var sut = new CityInfoManager(mockCityInfoRepository.Object);

            var city = sut.GetCity(1, false);

            Assert.That(city.Name, Is.EqualTo("TestCity1"));
        }

    }
}
