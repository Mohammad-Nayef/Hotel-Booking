﻿using HotelBooking.Db.Tables;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Db
{
    internal class HotelsBookingDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<HotelTable> Hotels { get; set; }
        public DbSet<CityTable> Cities { get; set; }
        public DbSet<ImageTable> Images { get; set; }
        public DbSet<RoomTable> Rooms { get; set; }
        public DbSet<DiscountTable> Discounts { get; set; }
        public DbSet<VisitTable> Visits { get; set; }
        public DbSet<BookingTable> Bookings { get; set; }
        public DbSet<CartItemTable> CartItems { get; set; }
        public DbSet<HotelReviewTable> HotelReviews { get; set; }
        public DbSet<UserTable> Users { get; set; }
        public DbSet<RoleTable> Roles { get; set; }
        public DbSet<HotelForAdminDTO> HotelsForAdmin { get; set; }
        public DbSet<RoomForAdminDTO> RoomsForAdmin { get; set; }
        public DbSet<CityForAdminDTO> CitiesForAdmin { get; set; }

        public HotelsBookingDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SqlServer"))
                .LogTo(Console.Write);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_HotelsForAdmin");
            });

            modelBuilder.Entity<RoomForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_RoomsForAdmin");
            });

            modelBuilder.Entity<CityForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_CitiesForAdmin");
            });
        }
    }
}
