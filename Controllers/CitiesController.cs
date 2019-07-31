using AutoMapper;
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

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDetail>>(cityEntities);

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
                var cityResult = Mapper.Map<CityDetail>(city);
                return Ok(cityResult);
            } 

            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointsOfInterestDetail>(city);
            return Ok(cityWithoutPointsOfInterestResult);
        }

    }
}
