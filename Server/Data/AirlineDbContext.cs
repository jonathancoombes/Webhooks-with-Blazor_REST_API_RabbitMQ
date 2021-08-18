using SpaceXAirlines.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Server.Data
{    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {
        }
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }

    }
}
