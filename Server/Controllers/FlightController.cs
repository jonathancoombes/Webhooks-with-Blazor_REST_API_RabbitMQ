using SpaceXAirlines.Server.Data;
using SpaceXAirlines.Server.MessageBus;
using SpaceXAirlines.Server.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SpaceXAirlines.Shared.Dto;
using static SpaceXAirlines.Server.Constants.Constants;

namespace SpaceXAirlines.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBus;
        
        private List<FlightDetailReadDto> AllFlights = new();

        public FlightController(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBus)
        {
            _context = context;
            _mapper = mapper;
            _messageBus = messageBus;
           }

        [HttpPost]
        public ActionResult<FlightDetailReadDto> CreateFlightDetail(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flightDetail =
                _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailCreateDto.FlightCode);

            if (flightDetail == null)
            {
                
                try
                {

                    _context.Add(_mapper.Map<FlightDetail>(flightDetailCreateDto));
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);

                }

                var flightDetailReadDto =
                    _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailCreateDto.FlightCode);

                return CreatedAtRoute(nameof(GetFlightDetailByFlightCode),
                    new {flightCode = flightDetailReadDto.FlightCode}, flightDetailReadDto);
            }
            else
            {
                return NoContent();

            }
        }


        [HttpGet(Name="GetAllFlights")]
        public ActionResult<List<FlightDetailReadDto>> GetAllFlights()
        {
            
            var flights = _context.FlightDetails.ToList();

            if (flights.Count!=0)
            {
                foreach (var flight in flights)
                {
                  var result = _mapper.Map<FlightDetailReadDto>(flight);
                    AllFlights.Add(result);
                }
            }
            return Ok(AllFlights);
        }



        [HttpGet("{flightCode}", Name = "GetFlightDetailByFlightCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailByFlightCode(string flightCode)
        {

            var flightDetail = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightCode);

            if (flightDetail != null)
            {

                return Ok(_mapper.Map<FlightDetailReadDto>(flightDetail));

            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{flightCode}")]
        public ActionResult<NotificationMessageDto> UpdateFlightDetail(FlightDetailUpdateDto flightDetailUpdateDto)
        {

            var flightDetail = _context.FlightDetails.FirstOrDefault(f => f.FlightCode == flightDetailUpdateDto.FlightCode);

            decimal oldPrice = flightDetail.Price;


            if (flightDetail != null)
            {
                _mapper.Map(flightDetailUpdateDto, flightDetail);

                try
                {
                    _context.SaveChanges();
                    

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
                    else
                    {
                        Console.WriteLine("No Price Change!");
                    }

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
            {
                return NoContent();
            }
        }
    }
    
}