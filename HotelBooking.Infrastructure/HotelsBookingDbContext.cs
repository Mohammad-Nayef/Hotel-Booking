using HotelBooking.Domain.Configurations;
using HotelBooking.Domain.Models.City;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBooking.Infrastructure
{
    /// <summary>
    /// Main DbContext of the project.
    /// </summary>
    internal class HotelsBookingDbContext : DbContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public HotelsBookingDbContext(IOptions<ConnectionStrings> connectionStringsOptions)
        {
            _connectionStrings = connectionStringsOptions.Value;
        }

        public DbSet<HotelTable> Hotels { get; set; }
        public DbSet<CityTable> Cities { get; set; }
        public DbSet<ImageTable> Images { get; set; }
        public DbSet<RoomTable> Rooms { get; set; }
        public DbSet<DiscountTable> Discounts { get; set; }
        public DbSet<HotelVisitTable> HotelVisits { get; set; }
        public DbSet<BookingTable> Bookings { get; set; }
        public DbSet<CartItemTable> CartItems { get; set; }
        public DbSet<HotelReviewTable> HotelReviews { get; set; }
        public DbSet<UserTable> Users { get; set; }
        public DbSet<RoleTable> Roles { get; set; }
        public DbSet<HotelForAdminDTO> HotelsForAdmin { get; set; }
        public DbSet<RoomForAdminDTO> RoomsForAdmin { get; set; }
        public DbSet<CityForAdminDTO> CitiesForAdmin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionStrings.SqlServer)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .MapViews()
                .ConfigureTables()
                .ConfigureRelations()
                .SeedTables();
        }
    }
}
