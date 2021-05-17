using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TheWeatherStationAPI.Models;

namespace TheWeatherStationAPI.Hub
{
    public class LiveHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendLiveUpdate(WeatherObservation weatherObservation)
        {
            await Clients.All.SendAsync("ReceiveMessage", weatherObservation);
        }
    }
}
