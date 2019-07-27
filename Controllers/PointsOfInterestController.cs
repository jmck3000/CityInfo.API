using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {

        /// <summary>
        /// Returns all Points Of Interest for a City.
        /// </summary>
        /// <param name="cityId">City Id</param> 
        /// <example>GET api/cities/1/pointsofinterest</example> 
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }


        /// <summary>
        /// Returns one PointOfInterest for one City.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="id">PointOfInterest Id</param> 
        /// <example>GET api/cities/1/pointsofinterest/1</example> 
        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(point => point.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }


        /// <summary>
        /// Insert PointOfInterest for one City.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="pointOfInterest">PointOfInterestForCreationDetail model</param> 
        /// <example>POST api/cities/3/pointsofinterest</example> 
        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDetail pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStrore.Current.Cities.SelectMany(
                city2 => city2.PointsOfInterest).Max(points => points.Id);

            var finalPointOfInterest = new PointOfInterestDetail()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }


        /// <summary>
        /// Update PointOfInterest for one City.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="id">PointOfInterest Id</param> 
        /// <param name="pointOfInterest">PointOfInterestForUpdateDetail model</param> 
        /// <example>PUT api/cities/1/pointsofinterest/1</example> 
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDetail pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(point => point.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }


        /// <summary>
        /// Update PointOfInterest for one City.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="id">PointOfInterest Id</param> 
        /// <param name="pointOfInterest">PointOfInterestForUpdateDetail model</param> 
        /// <example>PATCH api/cities/1/pointsofinterest/1</example> 
        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDetail> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(point => point.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDetail()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }

        /// <summary>
        /// Delete a PointOfInterest for one City.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <param name="id">PointOfInterest Id</param> 
        /// <example>PATCH api/cities/1/pointsofinterest/1</example> 
        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {

            var city = CitiesDataStrore.Current.Cities.FirstOrDefault(citys => citys.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(point => point.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            return NoContent();
        }


    }
}
