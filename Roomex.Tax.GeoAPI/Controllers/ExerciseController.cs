using System.Net;
using Microsoft.AspNetCore.Mvc;
using Roomex.Task.Core;
using Roomex.Task.GeoAPI.Requests;
using Roomex.Task.GeoAPI.Responses;

namespace Roomex.Task.GeoAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger<ExerciseController> _logger;
        private readonly IDistanceCalculator _calculator;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="calculator"></param>
        public ExerciseController(ILogger<ExerciseController> logger,IDistanceCalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }
        /// <summary>
        /// Calculate the distance in Kilometers between the two different GeoLocation
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        [HttpPost("CalculateDistance")]
        public IActionResult CalculateDistance([FromBody]DistanceRequest distance)
        {
            try
            {
                var result = _calculator.CalculateDistance(distance.Origin, distance.Target,distance.Measure);
                return Ok(new DistanceResponse(result,distance.Measure));
            }
            catch (Exception e)
            {
                _logger.LogError("Exception while calculating the distance between two points {msg}", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
