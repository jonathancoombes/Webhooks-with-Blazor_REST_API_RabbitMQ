using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Server.Models
{
    public class FlightDetail
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FlightCode { get; set; }
        [Required]
        [Column(TypeName ="decimal(6, 2)")]
        public decimal Price { get; set; }
    }
}
