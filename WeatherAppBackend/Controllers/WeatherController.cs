using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{globalIdLocal}")]
        public async Task<ActionResult<List<ForecastModel>>> GetForecast(int globalIdLocal)
        {
            var forecast = await _weatherService.GetForecast(globalIdLocal);

            if (forecast == null || forecast.Count == 0)
                return NotFound();

            return Ok(forecast);
        }
    }
}
