using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWeatherStationAPI.Models;
using WebApi.Data;

namespace TheWeatherStationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private MemoryRepository _repository;


        public WeatherForecastController()
        {
            _repository = MemoryRepository.GetInstance();
        }


        // GET:
        [HttpGet]
        public ActionResult<List<TemperatureReading>> Get()
        {
            return _repository.TemperatureReadings;
        }

        // POST:
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public ActionResult<TemperatureReading> Post(TemperatureReading temperature)
        {
            if (temperature == null)
            {
                return BadRequest();
            }
            var newTemp = _repository.AddTemperatureReading(new TemperatureReading()
            {
                DateTime = temperature.DateTime,
                Temperature = temperature.Temperature,
                Humidity = temperature.Humidity,
                AirPressure = temperature.AirPressure,
            });

            return CreatedAtAction("Get", new { id = newTemp.TemperatureReadingId }, newTemp);
        }
    }
}
