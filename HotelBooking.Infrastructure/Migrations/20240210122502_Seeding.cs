using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedUsersAndTheirRoles(migrationBuilder);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "CreationDate", "ModificationDate", "Name", "PostOffice" },
                values: new object[,]
                {
                    { new Guid("06f823bf-7b05-4953-4160-08dc299de452"), "France", new DateTime(2024, 2, 9, 18, 36, 25, 836, DateTimeKind.Unspecified).AddTicks(2319), new DateTime(2024, 2, 9, 18, 57, 18, 435, DateTimeKind.Unspecified).AddTicks(4543), "Paris", "string" },
                    { new Guid("8781a4f1-7d9a-4081-4162-08dc299de452"), "USA", new DateTime(2024, 2, 9, 18, 38, 32, 755, DateTimeKind.Unspecified).AddTicks(1763), new DateTime(2024, 2, 9, 18, 38, 32, 755, DateTimeKind.Unspecified).AddTicks(1766), "New York", "string" },
                    { new Guid("bc007ecd-4c13-4f90-4161-08dc299de452"), "UK", new DateTime(2024, 2, 9, 18, 38, 22, 85, DateTimeKind.Unspecified).AddTicks(4140), new DateTime(2024, 2, 9, 18, 38, 22, 85, DateTimeKind.Unspecified).AddTicks(4143), "London", "string" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "BriefDescription", "CityId", "CreationDate", "FullDescription", "Geolocation", "ModificationDate", "Name", "OwnerName", "StarRating" },
                values: new object[,]
                {
                    { new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), "A 4-star hotel located above Tower Hill Underground Station.", new Guid("bc007ecd-4c13-4f90-4161-08dc299de452"), new DateTime(2024, 2, 9, 18, 54, 53, 685, DateTimeKind.Unspecified).AddTicks(569), "Experience luxury and convenience at citizenM Tower of London, located above Tower Hill Underground Station. Enjoy stunning views of the Thames, Tower of London, and Tower Bridge from our 370 rooms. With 24/7 food and beverage service, free super-fast WiFi, and room controls via iPad or our app, your stay will be unforgettable.", "51.510223410295524,-0.07644353237381915", new DateTime(2024, 2, 9, 20, 45, 21, 12, DateTimeKind.Unspecified).AddTicks(2021), "citizenM Tower Of London hotel", "citizenM", 4f },
                    { new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), "A 4-star hotel offering panoramic views of Paris.", new Guid("06f823bf-7b05-4953-4160-08dc299de452"), new DateTime(2024, 2, 9, 18, 56, 58, 173, DateTimeKind.Unspecified).AddTicks(3565), "The 4-star Pullman Paris Tour Eiffel hotel offers contemporary guest rooms with panoramic views of Paris, some with stunning views of the Eiffel Tower or the garden. Enjoy Californian-style cuisine with French flavours at the hotel's restaurant, Frame, or dine on the terrace. A fitness room with cardio equipment is open 24 hours a day.", "48.85567419020331,2.2928680490125637", new DateTime(2024, 2, 9, 18, 56, 58, 173, DateTimeKind.Unspecified).AddTicks(3570), "Pullman Paris Tour Eiffel", "Pullman", 4.4f },
                    { new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), "A 4-star hotel in the heart of Times Square.", new Guid("8781a4f1-7d9a-4081-4162-08dc299de452"), new DateTime(2024, 2, 9, 19, 3, 10, 588, DateTimeKind.Unspecified).AddTicks(5700), "Stay in the heart of Times Square at our hotel with panoramic views from oversized guestrooms and sky-level restaurant. Enjoy Broadway's iconic theaters, shopping, and restaurants just outside the door.", "40.7566480079687,-73.98881546193508", new DateTime(2024, 2, 9, 19, 3, 10, 588, DateTimeKind.Unspecified).AddTicks(5704), "Hilton New York Times Square", "Hilton", 4.2f }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "AmountPercent", "EndingDate", "HotelId", "Reason", "StartingDate" },
                values: new object[,]
                {
                    { new Guid("294d2a20-6ac6-45b1-40c0-08dc29a25c37"), 5f, new DateTime(2024, 3, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), "New season", new DateTime(2024, 2, 9, 19, 6, 19, 661, DateTimeKind.Unspecified) },
                    { new Guid("2e634c0c-f29b-4eed-40c1-08dc29a25c37"), 10f, new DateTime(2024, 3, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), "New season", new DateTime(2024, 2, 20, 19, 6, 19, 661, DateTimeKind.Unspecified) },
                    { new Guid("75aef3a5-5f6a-48ea-40c2-08dc29a25c37"), 10f, new DateTime(2024, 3, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), "New season", new DateTime(2024, 2, 9, 19, 6, 19, 661, DateTimeKind.Unspecified) },
                    { new Guid("7ce31ec2-eb7c-4010-40c3-08dc29a25c37"), 5f, new DateTime(2024, 3, 9, 19, 6, 19, 661, DateTimeKind.Unspecified), new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), "New season", new DateTime(2024, 2, 20, 19, 6, 19, 661, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "HotelReviews",
                columns: new[] { "Id", "Content", "CreationDate", "HotelId", "UserId" },
                values: new object[] { new Guid("10c38d30-f6f1-4e17-ec5d-08dc29ae8167"), "Not bad", new DateTime(2024, 2, 9, 20, 34, 25, 182, DateTimeKind.Unspecified).AddTicks(8004), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") });

            migrationBuilder.InsertData(
                table: "HotelVisits",
                columns: new[] { "Id", "Date", "HotelId", "UserId" },
                values: new object[,]
                {
                    { new Guid("023f241b-884e-46aa-bd3a-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 16, 779, DateTimeKind.Unspecified).AddTicks(62), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("2537d5a5-c173-4d42-bd3e-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 18, 15, DateTimeKind.Unspecified).AddTicks(8790), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("5341a728-6bf3-4816-bd39-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 14, 575, DateTimeKind.Unspecified).AddTicks(9097), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("5bf50598-4354-4bc5-bd3b-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 17, 211, DateTimeKind.Unspecified).AddTicks(2413), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("5f84e84f-a559-479f-bd41-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 19, 31, DateTimeKind.Unspecified).AddTicks(2782), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("7baa02ac-e3ab-46e0-bd40-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 18, 384, DateTimeKind.Unspecified).AddTicks(4805), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("9d49c69c-d98f-4ac9-bd3d-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 17, 717, DateTimeKind.Unspecified).AddTicks(4073), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("ccba8bfb-c2a0-4f21-bd38-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 2, 4, 360, DateTimeKind.Unspecified).AddTicks(5808), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("de5574fb-8e28-4431-bd3f-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 18, 199, DateTimeKind.Unspecified).AddTicks(4640), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("e4fe58fb-ccd0-46a2-bd3c-08dc29a9fc95"), new DateTime(2024, 2, 9, 20, 16, 17, 561, DateTimeKind.Unspecified).AddTicks(2381), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultsCapacity", "BriefDescription", "ChildrenCapacity", "CreationDate", "HotelId", "ModificationDate", "Number", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), 2, "Some text", 3, new DateTime(2024, 2, 9, 19, 25, 25, 924, DateTimeKind.Unspecified).AddTicks(4730), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 25, 25, 924, DateTimeKind.Unspecified).AddTicks(5743), 1.0, 50.00m, "Family" },
                    { new Guid("3914316b-1b87-46f7-294f-08dc29a4de37"), 2, "Some text", 2, new DateTime(2024, 2, 9, 19, 27, 43, 963, DateTimeKind.Unspecified).AddTicks(7468), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 27, 43, 963, DateTimeKind.Unspecified).AddTicks(7475), 3.0, 75.00m, "Average" },
                    { new Guid("601a0d84-0435-4221-294e-08dc29a4de37"), 2, "Some text", 0, new DateTime(2024, 2, 9, 19, 27, 28, 669, DateTimeKind.Unspecified).AddTicks(1979), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 27, 28, 669, DateTimeKind.Unspecified).AddTicks(1982), 2.0, 100.00m, "Luxury" },
                    { new Guid("a914be7e-c545-4784-2948-08dc29a4de37"), 2, "Some text", 0, new DateTime(2024, 2, 9, 19, 25, 46, 76, DateTimeKind.Unspecified).AddTicks(2941), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 25, 46, 76, DateTimeKind.Unspecified).AddTicks(2945), 2.0, 100.00m, "Luxury" },
                    { new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"), 3, "Some text", 2, new DateTime(2024, 2, 9, 19, 26, 3, 594, DateTimeKind.Unspecified).AddTicks(2376), new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 3, 594, DateTimeKind.Unspecified).AddTicks(2379), 3.0, 75.00m, "Average" },
                    { new Guid("b3f52184-3330-4750-294c-08dc29a4de37"), 2, "Some text", 3, new DateTime(2024, 2, 9, 19, 26, 58, 930, DateTimeKind.Unspecified).AddTicks(8602), new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 58, 930, DateTimeKind.Unspecified).AddTicks(8604), 1.0, 50.00m, "Family" },
                    { new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"), 2, "Some text", 0, new DateTime(2024, 2, 9, 19, 26, 39, 972, DateTimeKind.Unspecified).AddTicks(3640), new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 39, 972, DateTimeKind.Unspecified).AddTicks(3645), 2.0, 100.00m, "Luxury" },
                    { new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"), 3, "Some text", 2, new DateTime(2024, 2, 9, 19, 26, 22, 383, DateTimeKind.Unspecified).AddTicks(9118), new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 26, 22, 383, DateTimeKind.Unspecified).AddTicks(9124), 3.0, 75.00m, "Average" },
                    { new Guid("f339b369-2a05-4eea-294d-08dc29a4de37"), 2, "Some text", 3, new DateTime(2024, 2, 9, 19, 27, 15, 270, DateTimeKind.Unspecified).AddTicks(8419), new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"), new DateTime(2024, 2, 9, 19, 27, 15, 270, DateTimeKind.Unspecified).AddTicks(8421), 1.0, 50.00m, "Family" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreationDate", "EndingDate", "Price", "RoomId", "StartingDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("3d027397-3903-4613-699c-08dc29af9712"), new DateTime(2024, 2, 9, 20, 48, 48, 607, DateTimeKind.Unspecified).AddTicks(6872), new DateTime(2024, 12, 20, 20, 35, 50, 160, DateTimeKind.Unspecified), 16102.50m, new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"), new DateTime(2024, 5, 9, 20, 35, 50, 160, DateTimeKind.Unspecified), new Guid("5f827131-b075-4fca-b35f-74e2957f5525") },
                    { new Guid("84a82828-e978-4264-699b-08dc29af9712"), new DateTime(2024, 2, 9, 20, 46, 39, 214, DateTimeKind.Unspecified).AddTicks(1140), new DateTime(2024, 4, 20, 20, 35, 50, 160, DateTimeKind.Unspecified), 3063.75m, new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"), new DateTime(2024, 3, 9, 20, 35, 50, 160, DateTimeKind.Unspecified), new Guid("5f827131-b075-4fca-b35f-74e2957f5525") },
                    { new Guid("92c48f96-7fc1-44b7-4d4d-08dc29aef958"), new DateTime(2024, 2, 9, 20, 37, 46, 374, DateTimeKind.Unspecified).AddTicks(4945), new DateTime(2024, 2, 20, 20, 35, 50, 160, DateTimeKind.Unspecified), 570.00m, new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), new DateTime(2024, 2, 9, 20, 35, 50, 160, DateTimeKind.Unspecified), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") },
                    { new Guid("a5198671-9494-427e-699a-08dc29af9712"), new DateTime(2024, 2, 9, 20, 42, 10, 907, DateTimeKind.Unspecified).AddTicks(4496), new DateTime(2024, 4, 20, 20, 35, 50, 160, DateTimeKind.Unspecified), 2042.50m, new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"), new DateTime(2024, 3, 9, 20, 35, 50, 160, DateTimeKind.Unspecified), new Guid("6198dc72-410c-48e6-9253-08dc29a983e6") }
                });
        }

        private static void SeedUsersAndTheirRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"""
                INSERT INTO users (Email, FirstName, Id, LastName, Password, Username)
                VALUES 
                    ('cs.moh.nayef@gmail.com',
                    'Mohammad',
                    '5f827131-b075-4fca-b35f-74e2957f5525',
                    'Nayef',
                    'ABv9DH+WserPEDnHrXZIvgKuQs6A+/JZkpDf4Cmdgt7Ba+J7oyLk0fQg8+vQEKqwNg==',
                    'admin'),
                    ('test@test',
                    'Regular',
                    'FAC6DF83-006C-4F18-2BB9-08DC2A28BB11',
                    'User',
                    'ACnWnIJzXcFnSkOLJPZsq7rW5g6Spc0NLBHr5XUgy0t0e2rXzvRNjkp0Kyhnh5HcKg==',
                    'test'),
                    (
                    '211026@ppu.edu.ps', 
                    'Mohammad', 
                    '6198DC72-410C-48E6-9253-08DC29A983E6', 
                    'Nayef', 
                    'AMQkE9ExsPl2VONSpjlytXY08vbN0qo08+DfmItrw2H5oZk0c6kMrIGoodCnPjo5iA==',
                    'mohammad_nayef')

                INSERT INTO roles (Id, Name)
                VALUES (
                    'e4e17bb1-654f-475c-a145-f8320dc5f3cf',
                    'Admin'
                ), (
                    '6fd10771-c8fe-4873-998a-c78a61e5b79c',
                    'RegularUser'
                );

                INSERT INTO UsersRoles (RolesId, UsersId)
                VALUES 
                    ('6FD10771-C8FE-4873-998A-C78A61E5B79C', '6198DC72-410C-48E6-9253-08DC29A983E6'),
                    ('6FD10771-C8FE-4873-998A-C78A61E5B79C', 'FAC6DF83-006C-4F18-2BB9-08DC2A28BB11'),
                    ('E4E17BB1-654F-475C-A145-F8320DC5F3CF', '5F827131-B075-4FCA-B35F-74E2957F5525');
                
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("3d027397-3903-4613-699c-08dc29af9712"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("84a82828-e978-4264-699b-08dc29af9712"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("92c48f96-7fc1-44b7-4d4d-08dc29aef958"));

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: new Guid("a5198671-9494-427e-699a-08dc29af9712"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("294d2a20-6ac6-45b1-40c0-08dc29a25c37"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("2e634c0c-f29b-4eed-40c1-08dc29a25c37"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("75aef3a5-5f6a-48ea-40c2-08dc29a25c37"));

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: new Guid("7ce31ec2-eb7c-4010-40c3-08dc29a25c37"));

            migrationBuilder.DeleteData(
                table: "HotelReviews",
                keyColumn: "Id",
                keyValue: new Guid("10c38d30-f6f1-4e17-ec5d-08dc29ae8167"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("023f241b-884e-46aa-bd3a-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("2537d5a5-c173-4d42-bd3e-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("5341a728-6bf3-4816-bd39-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("5bf50598-4354-4bc5-bd3b-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("5f84e84f-a559-479f-bd41-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("7baa02ac-e3ab-46e0-bd40-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("9d49c69c-d98f-4ac9-bd3d-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("ccba8bfb-c2a0-4f21-bd38-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("de5574fb-8e28-4431-bd3f-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "HotelVisits",
                keyColumn: "Id",
                keyValue: new Guid("e4fe58fb-ccd0-46a2-bd3c-08dc29a9fc95"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("3914316b-1b87-46f7-294f-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("601a0d84-0435-4221-294e-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("a914be7e-c545-4784-2948-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b3f52184-3330-4750-294c-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("e8a1a3e6-d8da-4928-294b-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f15bd95c-8746-4236-294a-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f339b369-2a05-4eea-294d-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("98123ca9-624e-4743-1268-08dc29a09a1f"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("b7535eac-a5b4-49bb-1269-08dc29a09a1f"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("28e17d69-9e0e-44a3-2947-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b3f07752-2d97-4b2c-2949-08dc29a4de37"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("06f823bf-7b05-4953-4160-08dc299de452"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8781a4f1-7d9a-4081-4162-08dc299de452"));

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("931ed0df-7b97-453d-1267-08dc29a09a1f"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bc007ecd-4c13-4f90-4161-08dc299de452"));
        }
    }
}
