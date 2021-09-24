using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceXAirlines.Server.Data;
using SpaceXAirlines.Server.Migrations;
using SpaceXAirlines.Server.Models;
using SpaceXAirlines.Shared.Dto;
using Exception = System.Exception;

namespace SpaceXAirlines.Server.Repository
{
    public class FlightRepository: IFlightRepository
    {
        private readonly AirlineDbContext _context;

        public FlightRepository(AirlineDbContext context )
        {
           _context = context;
        }

        public bool CreateFlightDetail(FlightDetail flightDetail)
        {
            if (flightDetail != null)
            {
                try 
                {
                    _context.Add(flightDetail);
                    _context.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                   throw new Exception(e.Message);
                };
            }

            return false;
        }

        public List<FlightDetail> GetAllFlights()
        {
            var flights = new List<FlightDetail>();

            try
            {
                flights = _context.FlightDetails.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return flights;
        }

        public FlightDetail GetFlightDetailByFlightCode(string flightCode)
        {
            var flightDetail = new FlightDetail();

            try
            {
               flightDetail = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return flightDetail;
        }

        public bool UpdateFlightDetail(FlightDetail flightDetail)
        {
            if (flightDetail != null)
            {
                _context.FlightDetails.Attach(flightDetail);
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);                
                }
            }
            return false;
        }
    }
}
