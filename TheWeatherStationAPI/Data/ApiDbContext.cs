using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheWeatherStationAPI.Models;

namespace TheWeatherStationAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }
        public DbSet<WeatherObservation> WeatherObservations { get; set; }
    }
}

