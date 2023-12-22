﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Db.Migrations
{
    /// <inheritdoc />
    public partial class vw_CitiesForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE VIEW vw_CitiesForAdmin AS
                SELECT Id, Name, CountryName, PostOffice, CreationDate, ModificationDate,
                    (SELECT COUNT(*) FROM Hotels
                    WHERE Hotels.CityId = Cities.Id)
                    AS NumberOfHotels
                FROM Cities
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_CitiesForAdmin");
        }
    }
}
