using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using redesAPI.Entities;

namespace redesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherDBContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(WeatherDBContext context, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /WeatherForecast
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherEntity>>> GetWeatherForecasts()
        {
            return await _context.Weather.ToListAsync();
        }

        // GET: /WeatherForecast/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherEntity>> GetWeatherForecast(int id)
        {
            var weather = await _context.Weather.FindAsync(id);

            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        // POST: /WeatherForecast
        [HttpPost]
        public async Task<ActionResult<WeatherEntity>> CreateWeatherForecast(WeatherEntity weather)
        {
            _context.Weather.Add(weather);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeatherForecast), new { id = weather.Id }, weather);
        }

        // PUT: /WeatherForecast/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecast(int id, WeatherEntity weather)
        {
            if (id != weather.Id)
            {
                return BadRequest();
            }

            _context.Entry(weather).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /WeatherForecast/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weather = await _context.Weather.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            _context.Weather.Remove(weather);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.Weather.Any(e => e.Id == id);
        }
    }
}
