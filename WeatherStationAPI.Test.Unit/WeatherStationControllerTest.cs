using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheWeatherStationAPI.Controllers;
using TheWeatherStationAPI.Data;
using TheWeatherStationAPI.Hub;
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

            _uut = new WeatherStationController(_context,null);
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
            Assert.Equal(3, list.Value.Count);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WeatherStationController_GetLastThreeTemps_ReturnsNotFound()
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

            Assert.IsType<NotFoundResult>(list.Result);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WeatherStationController_GetTempByDate_NoDate()
        {
            var list = await _uut.GetTempByDate(null);

            Assert.IsType<BadRequestResult>(list.Result);
        }


        [Fact]
        public async void WeatherStationController_GetTempByDate_FoundObservation()
        {
            _context.Database.EnsureCreated();

            DateTime date = new DateTime(2021, 5, 15);

            WeatherObservation wo1 = new WeatherObservation();
            wo1.Temperature = 10;
            wo1.Date = date;

            _context.WeatherObservations.Add(wo1);
            _context.SaveChanges();

            var list = await _uut.GetTempByDate(date);

            Assert.Equal(10, list.Value.ElementAt(0).Temperature);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WeatherStationController_GetTempByDate_NoObservationInDatabase()
        {
            _context.Database.EnsureCreated();

            DateTime date = new DateTime(2021, 5, 15);

            var list = await _uut.GetTempByDate(date);

            Assert.IsType<NotFoundResult>(list.Result);

            _context.Database.EnsureDeleted();
        }


        [Fact]
        public async void WeatherStationController_GetTempByStartAndEndTime_StartAndEndTimeNull()
        {
            _context.Database.EnsureCreated();

            DateTime date = new DateTime(2021, 5, 15);

            WeatherObservation wo1 = new WeatherObservation();
            wo1.Temperature = 10;
            wo1.Date = date;

            _context.WeatherObservations.Add(wo1);
            _context.SaveChanges();

            var list = await _uut.GetTempByStartAndEndTime(null,null);

            Assert.IsType<BadRequestResult>(list.Result);

            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async void WeatherStationController_GetTempByStartAndEndTime_Found2Observation()
        {
            _context.Database.EnsureCreated();

            DateTime date1 = new DateTime(2021, 5, 15);
            DateTime date2 = new DateTime(2021, 5, 16);
            DateTime date3 = new DateTime(2021, 5, 18);

            List<WeatherObservation> weatherObservations = new List<WeatherObservation>();

            WeatherObservation wo1 = new WeatherObservation();
            wo1.Temperature = 6;
            wo1.Date = date1;
            weatherObservations.Add(wo1);

            WeatherObservation wo2 = new WeatherObservation();
            wo2.Temperature = 15;
            wo2.Date = date2;
            weatherObservations.Add(wo2);

            WeatherObservation wo3 = new WeatherObservation();
            wo3.Temperature = 35;
            wo3.Date = date3;
            weatherObservations.Add(wo3);

            foreach (var weatherObservation in weatherObservations)
            {
                _context.WeatherObservations.Add(weatherObservation);
            }

            _context.SaveChanges();

            var list = await _uut.GetTempByStartAndEndTime(date1, date2);

            Assert.Equal(6, list.Value.ElementAt(0).Temperature);
            Assert.Equal(15, list.Value.ElementAt(1).Temperature);
            Assert.Equal(2, list.Value.Count);

            _context.Database.EnsureDeleted();
        }

    }
}
