using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TheWeatherStationAPI.Models
{
    public class WeatherObservation
    {
        public WeatherObservation()
        { 
            Station = new Station();
        }

        [Key]
        public long TemperatureReadingId { get; set; }

        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double AirPressure { get; set; }
        public Station Station { get; set; }
    }
}
