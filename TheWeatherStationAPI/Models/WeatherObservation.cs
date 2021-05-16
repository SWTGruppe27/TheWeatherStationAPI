using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TheWeatherStationAPI.Models
{
    public class WeatherObservation
    {
        [Key]
        public int WeatherObservationId { get; set; }

        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double AirPressure { get; set; }
        public Station Station { get; set; }
    }
}
