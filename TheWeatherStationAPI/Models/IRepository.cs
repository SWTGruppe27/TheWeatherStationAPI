using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWeatherStationAPI.Models;

namespace WebApi.Models
{
    public interface IRepository
    {
        List<TemperatureReading> TemperatureReadings { get; }
        TemperatureReading this[long id] { get; }
        TemperatureReading AddTemperatureReading(TemperatureReading reservation);
        TemperatureReading UpdateTemperatureReading(TemperatureReading reservation);
        void DeleteTemperatureReading(long id);
    }
}
