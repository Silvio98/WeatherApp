using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        public LocationsController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<LocationModel>>> GetLocations()
        {
            var locations = await _weatherService.GetLocations();

            if (locations == null || locations.Count == 0)
                return NotFound();

            return Ok(locations);
        }
    }
}
