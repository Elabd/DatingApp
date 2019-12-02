using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatingApp.API.Models;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }
        [AllowAnonymous]

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!_context.WeatherForecasts.Any())
            {
                var rng = new Random();
                _context.WeatherForecasts.AddRange(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
              .ToArray());
                _context.SaveChanges();
            }
            return Ok(await _context.WeatherForecasts.ToListAsync());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var weather = await _context.WeatherForecasts.FirstOrDefaultAsync(w => w.Id == id);
            return Ok(weather);
        }
    }
}
