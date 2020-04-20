using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.ServiceManager;
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
        private ICityInfoManager _cityInfoManager;

        public CitiesController(ICityInfoRepository cityInfoRepository, ICityInfoManager cityInfoManager)
        {
            _cityInfoRepository = cityInfoRepository;
            _cityInfoManager = cityInfoManager;
        }

        /// <summary>
        /// Returns all Cities
        /// </summary>
        /// <example>GET api/cities</example> 
        [HttpGet()]
        public IActionResult GetCities()
        {
            var results = _cityInfoManager.GetCities();

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
