using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Db.Migrations
{
    /// <inheritdoc />
    public partial class @as : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            FROM Rooms;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_RoomsForAdmin");
        }
    }
}
