using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Shared.Dto
{
    public class FlightDetailCreateDto
    {
        [Required]
        public string FlightCode { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
