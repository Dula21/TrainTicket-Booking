using Microsoft.EntityFrameworkCore;

using Booking_TrainTickets.Core.Entities;

namespace TrainTicket_Booking.Core.Context
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //we need to have a DBset for our Databse Table

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Train> Trains { get; set; }
    
    }
}