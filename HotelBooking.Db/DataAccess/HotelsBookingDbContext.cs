﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Db.DataAccess
{
    public class HotelsBookingDbContext : DbContext
    {
        private readonly IConfiguration _config;

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
