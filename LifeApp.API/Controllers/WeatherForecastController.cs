using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private DataContext _context { get; }
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(DataContext context, ILogger<WeatherForecastController> logger)
        {
            this._context = context;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllWeatherForecasts()
        {
            var weatherSet = await _context.Weathers.ToListAsync();
            return Ok(weatherSet);
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeatherForecastById(int id)
        {
            var weather = await _context.Weathers.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(weather);
        }

        private static readonly string[] Summaries = new[]
                {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot-hott", "Sweltering", "Scorching"
        };


        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
    }
}
