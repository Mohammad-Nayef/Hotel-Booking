using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Db.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "CartItems",
                newName: "AddingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddingDate",
                table: "CartItems",
                newName: "CreationDate");
        }
    }
}
