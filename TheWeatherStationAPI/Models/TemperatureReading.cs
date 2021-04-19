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

        public DateTime DateTime
        {
            get
            {
                return DateTime;
            }
            set
            {
                DateTime = DateTime.Now;
            }
        }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public int AirPressure { get; set; }
    }
}
