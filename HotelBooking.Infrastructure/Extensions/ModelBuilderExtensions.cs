using System.Linq.Expressions;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.City;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Constants;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
        /// <summary>
        /// Map the database views with its relevant models.
        /// </summary>
        public static ModelBuilder MapViews(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_HotelsForAdmin");
            });

            modelBuilder.Entity<RoomForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_RoomsForAdmin");
            });

            modelBuilder.Entity<CityForAdminDTO>(entity =>
            {
                entity.HasNoKey()
                    .ToView("vw_CitiesForAdmin");
            });

            return modelBuilder;
        }

        /// <summary>
        /// Add database-level limits for lengths of string properties.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        public static ModelBuilder ConfigureTables(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ConfigureCityTable()
                .ConfigureDiscountTable()
                .ConfigureHotelTable()
                .ConfigureHotelReviewTable()
                .ConfigureRoomTable()
                .ConfigureUserTable()
                .ConfigureHotelVisitTable()
                .ConfigureCartItemTable();

            return modelBuilder;
        }

        private static ModelBuilder ConfigureCartItemTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItemTable>()
                .HasIndex(item => item.UserId);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureHotelVisitTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelVisitTable>()
                .HasIndex(visit => visit.Date);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureCityTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<CityTable>(
                city => city.Name, CityConstants.MaxNameLength);
            modelBuilder.ConfigureStringLength<CityTable>(
                city => city.CountryName, CityConstants.MaxCountryNameLength);
            modelBuilder.ConfigureStringLength<CityTable>(
                city => city.PostOffice, CityConstants.MaxPostOfficeLength);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureDiscountTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<DiscountTable>(
                discount => discount.Reason, DiscountConstants.MaxReasonLength);
            modelBuilder.Entity<DiscountTable>(discount =>
            {
                discount.HasIndex(discount => discount.StartingDate);
                discount.HasIndex(discount => discount.EndingDate);
            });


            return modelBuilder;
        }

        private static ModelBuilder ConfigureHotelTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.Name, HotelConstants.MaxNameLength);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.BriefDescription, HotelConstants.MaxBriefDescriptionLength);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.FullDescription, HotelConstants.MaxFullDescriptionLength);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.OwnerName, HotelConstants.MaxNameLength);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureHotelReviewTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<HotelReviewTable>(
                review => review.Content, HotelReviewConstants.MaxContentLength);
            modelBuilder.Entity<HotelReviewTable>()
                .HasIndex(review => review.HotelId);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureRoomTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<RoomTable>(
                room => room.Type, RoomConstants.MaxTypeLength);
            modelBuilder.ConfigureStringLength<RoomTable>(
                room => room.BriefDescription, RoomConstants.MaxBriefDescriptionLength);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureUserTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<UserTable>(
                user => user.FirstName, UserConstants.MaxNameLength);
            modelBuilder.ConfigureStringLength<UserTable>(
                user => user.LastName, UserConstants.MaxNameLength);
            modelBuilder.ConfigureStringLength<UserTable>(
                user => user.Username, UserConstants.MaxUsernameLength);
            modelBuilder.Entity<UserTable>()
                .HasIndex(user => user.Username);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureStringLength<TEntity>(
            this ModelBuilder modelBuilder,
            Expression<Func<TEntity, string>> property,
            int maxLength)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .Property(property)
                .HasMaxLength(maxLength);

            return modelBuilder;
        }

        public static ModelBuilder ConfigureRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(UserRoleTableConstants.Name);

            return modelBuilder;
        }

        /// <summary>
        /// Fill the database tables with initial data.
        /// </summary>
        public static ModelBuilder SeedTables(this ModelBuilder modelBuilder)
        {
            SeedCitiesTable(modelBuilder);
            SeedHotelsTable(modelBuilder);
            SeedDiscountsTable(modelBuilder);
            SeedRoomsTable(modelBuilder);
            SeedBookingsTable(modelBuilder);
            SeedHotelReviewsTable(modelBuilder);
            SeedHotelsVisitsTable(modelBuilder);

            return modelBuilder;
        }

        private static void SeedHotelsVisitsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelVisitTable>().HasData(
                new HotelVisitTable
                {
                    Id = Guid.Parse("CCBA8BFB-C2A0-4F21-BD38-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:02:04.3605808"),
                    HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("5341A728-6BF3-4816-BD39-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:14.5759097"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("023F241B-884E-46AA-BD3A-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:16.7790062"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("5BF50598-4354-4BC5-BD3B-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:17.2112413"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("E4FE58FB-CCD0-46A2-BD3C-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:17.5612381"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("9D49C69C-D98F-4AC9-BD3D-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:17.7174073"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("2537D5A5-C173-4D42-BD3E-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:18.0158790"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("DE5574FB-8E28-4431-BD3F-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:18.1994640"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("7BAA02AC-E3AB-46E0-BD40-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:18.3844805"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new HotelVisitTable
                {
                    Id = Guid.Parse("5F84E84F-A559-479F-BD41-08DC29A9FC95"),
                    Date = DateTime.Parse("2024-02-09 20:16:19.0312782"),
                    HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                });
        }

        private static void SeedHotelReviewsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelReviewTable>().HasData(
               new HotelReviewTable
               {
                   Id = Guid.Parse("10C38D30-F6F1-4E17-EC5D-08DC29AE8167"),
                   Content = "Not bad",
                   CreationDate = DateTime.Parse("2024-02-09 20:34:25.1828004"),
                   HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F"),
                   UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
               }
           );
        }

        private static void SeedBookingsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingTable>().HasData(
                new BookingTable
                {
                    Id = Guid.Parse("92C48F96-7FC1-44B7-4D4D-08DC29AEF958"),
                    CreationDate = DateTime.Parse("2024-02-09 20:37:46.3744945"),
                    StartingDate = DateTime.Parse("2024-02-09 20:35:50.1600000"),
                    EndingDate = DateTime.Parse("2024-02-20 20:35:50.1600000"),
                    Price = 570.00m,
                    RoomId = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new BookingTable
                {
                    Id = Guid.Parse("A5198671-9494-427E-699A-08DC29AF9712"),
                    CreationDate = DateTime.Parse("2024-02-09 20:42:10.9074496"),
                    StartingDate = DateTime.Parse("2024-03-09 20:35:50.1600000"),
                    EndingDate = DateTime.Parse("2024-04-20 20:35:50.1600000"),
                    Price = 2042.50m,
                    RoomId = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                    UserId = Guid.Parse("6198DC72-410C-48E6-9253-08DC29A983E6")
                },
                new BookingTable
                {
                    Id = Guid.Parse("84A82828-E978-4264-699B-08DC29AF9712"),
                    CreationDate = DateTime.Parse("2024-02-09 20:46:39.2141140"),
                    StartingDate = DateTime.Parse("2024-03-09 20:35:50.1600000"),
                    EndingDate = DateTime.Parse("2024-04-20 20:35:50.1600000"),
                    Price = 3063.75m,
                    RoomId = Guid.Parse("B3F07752-2D97-4B2C-2949-08DC29A4DE37"),
                    UserId = Guid.Parse("5F827131-B075-4FCA-B35F-74E2957F5525")
                },
                new BookingTable
                {
                    Id = Guid.Parse("3D027397-3903-4613-699C-08DC29AF9712"),
                    CreationDate = DateTime.Parse("2024-02-09 20:48:48.6076872"),
                    StartingDate = DateTime.Parse("2024-05-09 20:35:50.1600000"),
                    EndingDate = DateTime.Parse("2024-12-20 20:35:50.1600000"),
                    Price = 16102.50m,
                    RoomId = Guid.Parse("B3F07752-2D97-4B2C-2949-08DC29A4DE37"),
                    UserId = Guid.Parse("5F827131-B075-4FCA-B35F-74E2957F5525")
                }
            );
        }

        private static void SeedRoomsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomTable>().HasData(
               new RoomTable
               {
                   Id = Guid.Parse("28E17D69-9E0E-44A3-2947-08DC29A4DE37"),
                   Number = 1,
                   Type = "Family",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 3,
                   BriefDescription = "Some text",
                   PricePerNight = 50.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:25:25.9244730"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:25:25.9245743"),
                   HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("A914BE7E-C545-4784-2948-08DC29A4DE37"),
                   Number = 2,
                   Type = "Luxury",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 0,
                   BriefDescription = "Some text",
                   PricePerNight = 100.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:25:46.0762941"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:25:46.0762945"),
                   HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("B3F07752-2D97-4B2C-2949-08DC29A4DE37"),
                   Number = 3,
                   Type = "Average",
                   AdultsCapacity = 3,
                   ChildrenCapacity = 2,
                   BriefDescription = "Some text",
                   PricePerNight = 75.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:26:03.5942376"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:26:03.5942379"),
                   HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("F15BD95C-8746-4236-294A-08DC29A4DE37"),
                   Number = 3,
                   Type = "Average",
                   AdultsCapacity = 3,
                   ChildrenCapacity = 2,
                   BriefDescription = "Some text",
                   PricePerNight = 75.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:26:22.3839118"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:26:22.3839124"),
                   HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("E8A1A3E6-D8DA-4928-294B-08DC29A4DE37"),
                   Number = 2,
                   Type = "Luxury",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 0,
                   BriefDescription = "Some text",
                   PricePerNight = 100.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:26:39.9723640"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:26:39.9723645"),
                   HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("B3F52184-3330-4750-294C-08DC29A4DE37"),
                   Number = 1,
                   Type = "Family",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 3,
                   BriefDescription = "Some text",
                   PricePerNight = 50.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:26:58.9308602"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:26:58.9308604"),
                   HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("F339B369-2A05-4EEA-294D-08DC29A4DE37"),
                   Number = 1,
                   Type = "Family",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 3,
                   BriefDescription = "Some text",
                   PricePerNight = 50.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:27:15.2708419"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:27:15.2708421"),
                   HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("601A0D84-0435-4221-294E-08DC29A4DE37"),
                   Number = 2,
                   Type = "Luxury",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 0,
                   BriefDescription = "Some text",
                   PricePerNight = 100.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:27:28.6691979"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:27:28.6691982"),
                   HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
               },
               new RoomTable
               {
                   Id = Guid.Parse("3914316B-1B87-46F7-294F-08DC29A4DE37"),
                   Number = 3,
                   Type = "Average",
                   AdultsCapacity = 2,
                   ChildrenCapacity = 2,
                   BriefDescription = "Some text",
                   PricePerNight = 75.00m,
                   CreationDate = DateTime.Parse("2024-02-09 19:27:43.9637468"),
                   ModificationDate = DateTime.Parse("2024-02-09 19:27:43.9637475"),
                   HotelId = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F")
               }
           );
        }

        private static void SeedDiscountsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountTable>().HasData(
                new DiscountTable
                {
                    Id = Guid.Parse("294D2A20-6AC6-45B1-40C0-08DC29A25C37"),
                    Reason = "New season",
                    StartingDate = DateTime.Parse("2024-02-09 19:06:19.6610000"),
                    EndingDate = DateTime.Parse("2024-03-09 19:06:19.6610000"),
                    AmountPercent = 5,
                    HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F")
                },
                new DiscountTable
                {
                    Id = Guid.Parse("2E634C0C-F29B-4EED-40C1-08DC29A25C37"),
                    Reason = "New season",
                    StartingDate = DateTime.Parse("2024-02-20 19:06:19.6610000"),
                    EndingDate = DateTime.Parse("2024-03-09 19:06:19.6610000"),
                    AmountPercent = 10,
                    HotelId = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F")
                },
                new DiscountTable
                {
                    Id = Guid.Parse("75AEF3A5-5F6A-48EA-40C2-08DC29A25C37"),
                    Reason = "New season",
                    StartingDate = DateTime.Parse("2024-02-09 19:06:19.6610000"),
                    EndingDate = DateTime.Parse("2024-03-09 19:06:19.6610000"),
                    AmountPercent = 10,
                    HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
                },
                new DiscountTable
                {
                    Id = Guid.Parse("7CE31EC2-EB7C-4010-40C3-08DC29A25C37"),
                    Reason = "New season",
                    StartingDate = DateTime.Parse("2024-02-20 19:06:19.6610000"),
                    EndingDate = DateTime.Parse("2024-03-09 19:06:19.6610000"),
                    AmountPercent = 5,
                    HotelId = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F")
                }
            );
        }

        private static void SeedHotelsTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelTable>().HasData(
                new HotelTable
                {
                    Id = Guid.Parse("931ED0DF-7B97-453D-1267-08DC29A09A1F"),
                    Name = "citizenM Tower Of London hotel",
                    BriefDescription = "A 4-star hotel located above Tower Hill Underground Station.",
                    FullDescription = "Experience luxury and convenience at citizenM Tower of London, located above Tower Hill Underground Station. Enjoy stunning views of the Thames, Tower of London, and Tower Bridge from our 370 rooms. With 24/7 food and beverage service, free super-fast WiFi, and room controls via iPad or our app, your stay will be unforgettable.",
                    StarRating = 4,
                    OwnerName = "citizenM",
                    Geolocation = "51.510223410295524,-0.07644353237381915",
                    CreationDate = DateTime.Parse("2024-02-09 18:54:53.6850569"),
                    ModificationDate = DateTime.Parse("2024-02-09 20:45:21.0122021"),
                    CityId = Guid.Parse("BC007ECD-4C13-4F90-4161-08DC299DE452")
                },
                new HotelTable
                {
                    Id = Guid.Parse("98123CA9-624E-4743-1268-08DC29A09A1F"),
                    Name = "Pullman Paris Tour Eiffel",
                    BriefDescription = "A 4-star hotel offering panoramic views of Paris.",
                    FullDescription = "The 4-star Pullman Paris Tour Eiffel hotel offers contemporary guest rooms with panoramic views of Paris, some with stunning views of the Eiffel Tower or the garden. Enjoy Californian-style cuisine with French flavours at the hotel's restaurant, Frame, or dine on the terrace. A fitness room with cardio equipment is open 24 hours a day.",
                    StarRating = 4.4f,
                    OwnerName = "Pullman",
                    Geolocation = "48.85567419020331,2.2928680490125637",
                    CreationDate = DateTime.Parse("2024-02-09 18:56:58.1733565"),
                    ModificationDate = DateTime.Parse("2024-02-09 18:56:58.1733570"),
                    CityId = Guid.Parse("06F823BF-7B05-4953-4160-08DC299DE452")
                },
                new HotelTable
                {
                    Id = Guid.Parse("B7535EAC-A5B4-49BB-1269-08DC29A09A1F"),
                    Name = "Hilton New York Times Square",
                    BriefDescription = "A 4-star hotel in the heart of Times Square.",
                    FullDescription = "Stay in the heart of Times Square at our hotel with panoramic views from oversized guestrooms and sky-level restaurant. Enjoy Broadway's iconic theaters, shopping, and restaurants just outside the door.",
                    StarRating = 4.2f,
                    OwnerName = "Hilton",
                    Geolocation = "40.7566480079687,-73.98881546193508",
                    CreationDate = DateTime.Parse("2024-02-09 19:03:10.5885700"),
                    ModificationDate = DateTime.Parse("2024-02-09 19:03:10.5885704"),
                    CityId = Guid.Parse("8781A4F1-7D9A-4081-4162-08DC299DE452")
                }
            );
        }

        private static void SeedCitiesTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityTable>().HasData(
                new CityTable
                {
                    Id = Guid.Parse("06F823BF-7B05-4953-4160-08DC299DE452"),
                    Name = "Paris",
                    CountryName = "France",
                    PostOffice = "string",
                    CreationDate = DateTime.Parse("2024-02-09 18:36:25.8362319"),
                    ModificationDate = DateTime.Parse("2024-02-09 18:57:18.4354543")
                },
                new CityTable
                {
                    Id = Guid.Parse("BC007ECD-4C13-4F90-4161-08DC299DE452"),
                    Name = "London",
                    CountryName = "UK",
                    PostOffice = "string",
                    CreationDate = DateTime.Parse("2024-02-09 18:38:22.0854140"),
                    ModificationDate = DateTime.Parse("2024-02-09 18:38:22.0854143")
                },
                new CityTable
                {
                    Id = Guid.Parse("8781A4F1-7D9A-4081-4162-08DC299DE452"),
                    Name = "New York",
                    CountryName = "USA",
                    PostOffice = "string",
                    CreationDate = DateTime.Parse("2024-02-09 18:38:32.7551763"),
                    ModificationDate = DateTime.Parse("2024-02-09 18:38:32.7551766")
                }
            );
        }
    }
}
