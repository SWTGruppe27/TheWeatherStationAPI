using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TheWeatherStationAPI.Controllers;
using TheWeatherStationAPI.Data;
using TheWeatherStationAPI.Models;
using Xunit;

namespace WeatherStationAPI.Test.Unit
{
    public class WeatherStationControllerTest
    {
        private WeatherStationController _uut;
        private ApiDbContext _context;

        public WeatherStationControllerTest()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ApiDbContext>().UseSqlite(connection).Options;

            _context = new ApiDbContext(options);

            _uut = new WeatherStationController(_context);
        }

        [Fact]
        public async void WeatherStationController_GetLastThreeTemps_CorrectTemprature()
        {

            _context.Database.EnsureCreated();

            List<WeatherObservation> weatherObservations = new List<WeatherObservation>();
            
            WeatherObservation wo1 = new WeatherObservation();
            wo1.Temperature = 25;
            weatherObservations.Add(wo1);

            WeatherObservation wo2 = new WeatherObservation();
            wo2.Temperature = 15;
            weatherObservations.Add(wo2);

            WeatherObservation wo3 = new WeatherObservation();
            wo3.Temperature = 35;
            weatherObservations.Add(wo3);

            foreach (var weatherObservation in weatherObservations)
            {
                _context.WeatherObservations.Add(weatherObservation);
            }
            
            _context.SaveChanges();

            var list = await _uut.GetLastThreeTemps();

            Assert.Equal(25, list.Value.ElementAt(0).Temperature);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WeatherStationController_GetLastThreeTemps_Not()
        {

            _context.Database.EnsureCreated();

            List<WeatherObservation> weatherObservations = new List<WeatherObservation>();

            WeatherObservation wo1 = new WeatherObservation();
            wo1.Temperature = 25;
            weatherObservations.Add(wo1);

            WeatherObservation wo2 = new WeatherObservation();
            wo2.Temperature = 15;
            weatherObservations.Add(wo2);


            foreach (var weatherObservation in weatherObservations)
            {
                _context.WeatherObservations.Add(weatherObservation);
            }

            _context.SaveChanges();

            var list = await _uut.GetLastThreeTemps();

            Assert.Equal(, list.Value.ElementAt(0).Temperature);

            _context.Database.EnsureDeleted();
        }
    }
}
