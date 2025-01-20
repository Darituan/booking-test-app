using Microsoft.EntityFrameworkCore;

namespace BookingApp.Models
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
