using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWeatherStationAPI.Models
{
    public class Station
    {
        [Key]
        public int StationId { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
