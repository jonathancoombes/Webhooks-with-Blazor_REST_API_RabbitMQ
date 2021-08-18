using SpaceXAirlines.Shared.Dto;
using SpaceXAirlines.Server.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Server.Profiles
{
    public class FlightDetailProfile: Profile
    {
        public FlightDetailProfile()
        {

            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
                      

        }


    }
}
