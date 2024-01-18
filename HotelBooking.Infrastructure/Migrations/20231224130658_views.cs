using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE VIEW vw_HotelsForAdmin AS
                SELECT Id, Name, StarRating, OwnerName, CreationDate, ModificationDate,
                    (
                        SELECT COUNT(*) FROM Rooms
                        WHERE Rooms.HotelId = Hotels.Id
                    ) As NumberOfRooms
                FROM Hotels
                """);

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
            migrationBuilder.Sql("""
                drop VIEW vw_HotelsForAdmin AS
                """);

            migrationBuilder.Sql("""
                drop VIEW vw_CitiesForAdmin AS
                """);
        }
    }
}
