using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceXAirlines.Server.Models;
using SpaceXAirlines.Shared.Dto;

namespace SpaceXAirlines.Server.Repository
{
    public interface IFlightRepository
    {
        bool CreateFlightDetail(FlightDetail flightDetail);
        List<FlightDetail> GetAllFlights();
        FlightDetail GetFlightDetailByFlightCode(string flightCode);
        bool UpdateFlightDetail(FlightDetail flightDetail);

    }
}
