using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TheWeatherStationAPI.Models
{
    public class TemperatureReading
    {
        public long TemperatureReadingId { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double AirPressure { get; set; }
    }
}
