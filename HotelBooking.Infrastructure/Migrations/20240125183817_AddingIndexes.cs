using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndingDate",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_HotelVisits_Date",
                table: "HotelVisits",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_EndingDate",
                table: "Discounts",
                column: "EndingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_StartingDate",
                table: "Discounts",
                column: "StartingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_HotelVisits_Date",
                table: "HotelVisits");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_EndingDate",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_StartingDate",
                table: "Discounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndingDate",
                table: "Discounts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
