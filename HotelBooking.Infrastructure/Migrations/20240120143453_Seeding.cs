using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"""
                INSERT INTO users (Email, FirstName, Id, LastName, Password, Username)
                VALUES (
                    'cs.moh.nayef@gmail.com',
                    'Mohammad',
                    '5f827131-b075-4fca-b35f-74e2957f5525',
                    'Nayef',
                    'ABv9DH+WserPEDnHrXZIvgKuQs6A+/JZkpDf4Cmdgt7Ba+J7oyLk0fQg8+vQEKqwNg==',
                    'admin'
                );

                INSERT INTO roles (Id, Name)
                VALUES (
                    'e4e17bb1-654f-475c-a145-f8320dc5f3cf',
                    'Admin'
                ), (
                    '6fd10771-c8fe-4873-998a-c78a61e5b79c',
                    'RegularUser'
                );

                INSERT INTO {UserRoleTable.Name} (RolesId, UsersId)
                VALUES (
                    'e4e17bb1-654f-475c-a145-f8320dc5f3cf',
                    '5f827131-b075-4fca-b35f-74e2957f5525'
                );
                
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
