using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TheWeatherStationAPI.Data;
using TheWeatherStationAPI.Models;

namespace TheWeatherStationAPI.Controllers
{
    [ApiController, Authorize]
    [Route("[controller]")]
    public class WeatherStationController : ControllerBase
    {
        private ApiDbContext _context;

        public WeatherStationController(ApiDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        [Route("GetLastThreeTemps")]
        public async Task<ActionResult<List<WeatherObservation>>> GetLastThreeTemps()
        {
            var list = await _context.WeatherObservations
                .Include(w => w.Station)
                .OrderByDescending(w => w.Date).ToListAsync();

            if (list.Count < 3)
            {
                return NotFound();
            }

            List<WeatherObservation> listWithLastThreeTemps = new List<WeatherObservation>();
            listWithLastThreeTemps.Add(list.ElementAt(0));
            listWithLastThreeTemps.Add(list.ElementAt(1));
            listWithLastThreeTemps.Add(list.ElementAt(2));

            return listWithLastThreeTemps;
        }

        // GET:
        [HttpGet("GetTempByDate/{Date}", Name = "GetTempByDate")]
        //[Route("GetTempByDate")]
        public async Task<ActionResult<List<WeatherObservation>>> GetTempByDate(DateTime date)
        {
            var list = await _context.WeatherObservations.Where(d => d.Date.ToString("d") == date.ToString("d")).ToListAsync();

            if (list.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return list;
            }
        }

        //GET:
        [HttpGet("{startTime}/{endTime}", Name = "GetTempByStartAndEndTime")]
        //[Route("GetTempByDate")]
        public async Task<ActionResult<List<WeatherObservation>>> GetTempByStartAndEndTime(DateTime startTime, DateTime endTime)
        {
            var list = await _context.WeatherObservations.Where(d => d.Date >= startTime && d.Date <= endTime).ToListAsync();

            if (list.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return list;
            }
        }

        // POST:
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public ActionResult<WeatherObservation> Post(WeatherObservation temperature)
        {
            if (temperature == null)
            {
                return BadRequest();
            }
            var newTemp = (new WeatherObservation()
            {
                Date = temperature.Date,
                Station = new Station()
                {
                    Name = temperature.Station.Name,
                    Lat = temperature.Station.Lat,
                    Lon = temperature.Station.Lon
                },
                Temperature = Math.Round(temperature.Temperature, 1),
                Humidity = CheckHumidity(temperature.Humidity),
                AirPressure = Math.Round(temperature.AirPressure,1),
            });

            _context.Add(newTemp);
            _context.SaveChanges();

            return CreatedAtAction("GetLastThreeTemps", new { id = newTemp.WeatherObservationId }, newTemp);
        }

        private int CheckHumidity(int humidity)
        {
            if (humidity <0)
            {
                return 0;
            }
            else if (humidity > 100)
            {
                return 100;
            }
            else
            {
                return humidity;
            }
        }
    }
}
