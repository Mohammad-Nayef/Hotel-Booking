using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Db.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
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
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_HotelsForAdmin");
        }
    }
}
