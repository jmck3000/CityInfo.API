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

        /// <summary>
        /// Returns all Cities
        /// </summary>
        /// <example>GET api/cities</example> 
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStrore.Current.Cities);
        }

        /// <summary>
        /// Returns a City by Id
        /// </summary>
        /// <param name="id">City Id</param> 
        /// <example>GET api/cities/1</example> 
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = CitiesDataStrore.Current.Cities.FirstOrDefault(city => city.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);

          
        }

    }
}
