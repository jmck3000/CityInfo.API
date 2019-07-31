using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailServices,
            ICityInfoRepository cityInfoRepository)
        {

            _logger = logger;
            _mailService = mailServices;
            _cityInfoRepository = cityInfoRepository;
        }

        /// <summary>
        /// Returns all Points Of Interest for a City.
        /// </summary>
        /// <param name="cityId">City Id</param> 
        /// <example>GET api/cities/1/pointsofinterest</example> 
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

                var pointsOfInterestForCityResults =
                    Mapper.Map<IEnumerable<PointOfInterestDetail>>(pointsOfInterestForCity);

                return Ok(pointsOfInterestForCityResults);

            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A Problem happened while handling your repuest.");
            }
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

            try
            {
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

                if (pointOfInterest == null)
                {
                    return NotFound();
                }

                var pointOfInterestResult = Mapper.Map<PointOfInterestDetail>(pointOfInterest);

                return Ok(pointOfInterestResult);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A Problem happened while handling your repuest.");
            }
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

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }


            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDetail>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
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

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

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

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDetail>(pointOfInterestEntity);

         
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

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if(!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

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

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            _mailService.Send("Point of interest deleted.",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");

            return NoContent();
        }


    }
}
