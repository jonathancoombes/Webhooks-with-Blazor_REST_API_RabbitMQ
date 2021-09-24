using SpaceXAirlines.Server.Data;
using SpaceXAirlines.Server.MessageBus;
using SpaceXAirlines.Server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SpaceXAirlines.Server.Repository;
using SpaceXAirlines.Shared.Dto;
using static SpaceXAirlines.Server.Constants.Constants;

namespace SpaceXAirlines.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBus;
        
        private readonly List<FlightDetailReadDto> AllFlights = new();
        private readonly IFlightRepository _flight;

        public FlightController(IMapper mapper, IMessageBusClient messageBus, IFlightRepository flight)
        {
            _mapper = mapper;
            _messageBus = messageBus;
            _flight = flight;
        }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlightDetail(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flightDetail =_flight.GetFlightDetailByFlightCode(flightDetailCreateDto.FlightCode);

            if (flightDetail == null)
            {
                var flDetail = _mapper.Map<FlightDetail>(flightDetailCreateDto);
                var flightAdded = _flight.CreateFlightDetail(flDetail);

                if (flightAdded)
                {
                   return CreatedAtRoute(nameof(GetFlightDetailByFlightCode),
                    new {flightCode = flightDetail.FlightCode}, flightDetail);
                }
                
                return BadRequest();
            }
            return NoContent();
        }


        [HttpGet(Name="GetAllFlights")]
        public ActionResult<List<FlightDetailReadDto>> GetAllFlights()
        {
            var flights = _flight.GetAllFlights();

            if (flights.Count > 0)
            {
                foreach (var flight in flights)
                {
                  var result = _mapper.Map<FlightDetailReadDto>(flight);
                    AllFlights.Add(result);
                }
                return Ok(AllFlights);
            }

            return NotFound();
        }



        [HttpGet("{flightCode}", Name = "GetFlightDetailByFlightCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailByFlightCode(string flightCode)
        {
            var flightDetail = _flight.GetFlightDetailByFlightCode(flightCode);

            if (flightDetail != null)
            {
                return Ok(_mapper.Map<FlightDetailReadDto>(flightDetail));
            }
           
            return NotFound();

        }

        [HttpPut("{flightCode}")]
        public ActionResult<NotificationMessageDto> UpdateFlightDetail(FlightDetailUpdateDto flightDetailUpdateDto)
        {

            var flightDetail = _flight.GetFlightDetailByFlightCode(flightDetailUpdateDto.FlightCode);
            decimal oldPrice = flightDetail.Price;
            
            var updatedFlightDetail = _mapper.Map(flightDetailUpdateDto, flightDetail);

            if (updatedFlightDetail != null)
            {
                _flight.UpdateFlightDetail(updatedFlightDetail);

             if (oldPrice != flightDetail.Price)
                {
                    Console.WriteLine("Price Changed - Placing Message on bus");

                    var message = new NotificationMessageDto
                    { 
                        FlightCode = flightDetail.FlightCode,
                        OldPrice = oldPrice,
                        WebhookType = WebhookType.PriceChange.ToString()  
                    };

                    //Place on queue
                    _messageBus.SendMessage(message);

                    return Ok(message);
                }
                Console.WriteLine("No Price Change!");
                                    
                    return NoContent();
            }

            return NoContent();
            
        }
        
    }
    
}