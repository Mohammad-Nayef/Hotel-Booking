using HotelBooking.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Db
{
    internal class HotelsBookingDbContext : DbContext
    {
        private readonly IConfiguration _config;

        internal DbSet<HotelDTO> Hotels { get; set; }
        internal DbSet<LocationDTO> Locations { get; set; }
        internal DbSet<ImageDTO> Images { get; set; }
        internal DbSet<RoomDTO> Rooms { get; set; }
        internal DbSet<DiscountDTO> Discounts { get; set; }
        internal DbSet<VisitDTO> Visits { get; set; }
        internal DbSet<BookingDTO> Bookings { get; set; }
        internal DbSet<CartItemDTO> CartItems { get; set; }
        internal DbSet<HotelReviewDTO> HotelReviews { get; set; }
        internal DbSet<UserDTO> Users { get; set; }
        internal DbSet<RoleDTO> Roles { get; set; }

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
