using HotelBooking.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Db.DataAccess
{
    public class HotelsBookingDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }
        public DbSet<User> Users { get; set; }

        public HotelsBookingDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"));
        }
    }
}
