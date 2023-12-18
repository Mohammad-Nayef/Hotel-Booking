using HotelBooking.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Db
{
    public class HotelsBookingDbContext : DbContext
    {
        private readonly IConfiguration _config;

        internal DbSet<Hotel> Hotels { get; set; }
        internal DbSet<Location> Locations { get; set; }
        internal DbSet<Image> Images { get; set; }
        internal DbSet<Room> Rooms { get; set; }
        internal DbSet<Discount> Discounts { get; set; }
        internal DbSet<Visit> Visits { get; set; }
        internal DbSet<Booking> Bookings { get; set; }
        internal DbSet<CartItem> CartItems { get; set; }
        internal DbSet<HotelReview> HotelReviews { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Role> Roles { get; set; }

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
