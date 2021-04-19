using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWeatherStationAPI.Models;
using WebApi.Models;

namespace WebApi.Data
{
    public class MemoryRepository : IRepository
    {
        private static MemoryRepository instance = null;
        private readonly Dictionary<long, TemperatureReading> items;

        private MemoryRepository()
        {
            items = new Dictionary<long, TemperatureReading>();
            new List<TemperatureReading> {
                new TemperatureReading() { Temperature = 14.5}
            }.ForEach(r => AddTemperatureReading(r));
        }

        // Implement singleton design pattern
        static public MemoryRepository GetInstance()
        {
            if (instance == null)
                instance = new MemoryRepository();
            return instance;
        }

        public TemperatureReading this[long id] => items.ContainsKey(id) ? items[id] : null;

        public List<TemperatureReading> TemperatureReadings => items.Values.ToList();

        public TemperatureReading AddTemperatureReading(TemperatureReading temperatureReading)
        {
            if (temperatureReading.TemperatureReadingId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                temperatureReading.TemperatureReadingId = key;
            }
            items[temperatureReading.TemperatureReadingId] = temperatureReading;
            return temperatureReading;
        }

        public void DeleteTemperatureReading(long id) => items.Remove(id);

        public TemperatureReading UpdateTemperatureReading(TemperatureReading reservation) => AddTemperatureReading(reservation);
    }
}
