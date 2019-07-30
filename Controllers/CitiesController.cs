using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{

    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        /// <summary>
        /// Returns all Cities
        /// </summary>
        /// <example>GET api/cities</example> 
        [HttpGet()]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointsOfInterestDetail>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDetail
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }

            return Ok(results);
        }

        /// <summary>
        /// Returns a City by Id
        /// </summary>
        /// <param name="id">City Id</param> 
        /// <param name="includePointsOfInterest">bool/param> 
        /// <example>GET api/cities/1</example> 
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if(city == null)
            {
                return NotFound();
            }

            if(includePointsOfInterest)
            {
                var cityResult = new CityDetail()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach(var poi in city.PointOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                        new PointOfInterestDetail()
                        {
                            Id = poi.Id,
                            Name = poi.Name,
                            Description = poi.Description
                        });
                }

                return Ok(cityResult);

            } //if(includePointsOfInterest)

            var cityWithoutPointsOfInterestResult = new CityWithoutPointsOfInterestDetail()
            {
                Id = city.Id,
                Description = city.Description,
                Name = city.Name
            };

            return Ok(cityWithoutPointsOfInterestResult);
        }

    }
}
