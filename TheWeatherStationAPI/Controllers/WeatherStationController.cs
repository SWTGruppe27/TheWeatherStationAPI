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
    public class WeatherStationController : ControllerBase
    {
        private MemoryRepository _repository;


        public WeatherStationController()
        {
            _repository = MemoryRepository.GetInstance();
        }


        // GET:
        [HttpGet]
        public ActionResult<List<WeatherObservation>> Get()
        {
            return _repository.TemperatureReadings;
        }

        // GET:
        [HttpGet]
        [Route("GetLastTemp")]
        public ActionResult<WeatherObservation> GetLatestTemp()
        {
            return _repository.TemperatureReadings.ElementAt(_repository.TemperatureReadings.Count-1);
        }


        // GET:
        [HttpGet("{Date}", Name = "GetTemp")]
        //[Route("GetTempByDate")]
        public ActionResult<List<WeatherObservation>> GetTempByDate(string date)
        {
            List<WeatherObservation> item = new List<WeatherObservation>();

            foreach (var temperatureReadings in _repository.TemperatureReadings)
            {
                if (temperatureReadings.Date == date)
                {
                    item.Add(temperatureReadings);
                }
            }

            if (item.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return item;
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
            var newTemp = _repository.AddTemperatureReading(new WeatherObservation()
            {
                Date = temperature.Date,
                Time = temperature.Time,
                Temperature = temperature.Temperature,
                Humidity = temperature.Humidity,
                AirPressure = temperature.AirPressure,
            });

            return CreatedAtAction("Get", new { id = newTemp.TemperatureReadingId }, newTemp);
        }
    }
}
