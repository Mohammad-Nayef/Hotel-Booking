using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Views : Migration
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

            migrationBuilder.Sql("""
                                CREATE VIEW vw_RoomsForAdmin AS
                SELECT 
                    Id, Type, Number, AdultsCapacity, ChildrenCapacity, CreationDate, ModificationDate, 
                    CAST(
                        CASE
                            WHEN EXISTS (
                                SELECT 1
                                FROM Bookings
                                WHERE Bookings.RoomId = Rooms.Id
                                  AND GETDATE() BETWEEN Bookings.StartingDate AND Bookings.EndingDate) 
                                THEN 0
                            ELSE 1
                        END AS BIT
                    ) AS IsAvailable
                FROM Rooms
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_RoomsForAdmin, vw_CitiesForAdmin, vw_HotelsForAdmin");
        }
    }
}
