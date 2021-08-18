using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Shared.Dto
{
    public class FlightDetailReadDto
    {
        public int Id { get; set; }
        public string FlightCode { get; set; }
        public decimal Price { get; set; }

    }
}
