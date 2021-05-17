using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using TheWeatherStationAPI.Data;
using TheWeatherStationAPI.Hub;
using TheWeatherStationAPI.Models;
using System.Text.Json;

namespace TheWeatherStationAPI.Controllers
{
    [ApiController, Authorize]
    //[ApiController]
    [Route("[controller]")]
    public class WeatherStationController : ControllerBase
    {
        private ApiDbContext _context;
        private IHubContext<LiveHub> _hubContext;

        public WeatherStationController(ApiDbContext context, IHubContext<LiveHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET:
        [HttpGet("GetLastThreeTemps")]
        //[Route("GetLastThreeTemps")]
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
        [HttpGet("GetTempByDate/{date}")]
        //[Route("GetTempByDate")]
        public async Task<ActionResult<List<WeatherObservation>>> GetTempByDate(DateTime? date)
        {

            if (date == null)
            {
                return BadRequest();
            }

            DateTime temp = (DateTime)date;

            var list = await _context.WeatherObservations.Where(d => d.Date.Date == temp.Date)
                .Include(s => s.Station)
                .ToListAsync();

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
        [HttpGet("GetTempByStartAndEndTime/{startTime}/{endTime}")]
        //[Route("GetTempByDate")]
        public async Task<ActionResult<List<WeatherObservation>>> GetTempByStartAndEndTime(DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null || endTime == null)
            {
                return BadRequest();
            }

            DateTime sTime = (DateTime) startTime;
            DateTime eTime = (DateTime) endTime;

            var list = await _context.WeatherObservations.Where(d => d.Date.Date >= sTime.Date && d.Date.Date <= eTime.Date)
                .Include(s=>s.Station)
                .ToListAsync();

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
        public async Task<ActionResult<WeatherObservation>> Post(WeatherObservation temperature)
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
                AirPressure = Math.Round(temperature.AirPressure, 1),
            });

            _context.Add(newTemp);
            _context.SaveChanges();

            string json = JsonSerializer.Serialize(temperature);
            
            if (_hubContext != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", json);
            }
            
            return CreatedAtAction("GetLastThreeTemps", new { id = newTemp.WeatherObservationId }, newTemp);
        }

        private int CheckHumidity(int humidity)
        {
            if (humidity < 0)
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
