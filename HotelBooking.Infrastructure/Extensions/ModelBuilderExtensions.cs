using System.Linq.Expressions;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Models.City;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Extensions
{
    internal static class ModelBuilderExtensions
    {
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

        public static ModelBuilder AddPropertiesLengthLimit(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .ConfigureCityTable()
                .ConfigureDiscountTable()
                .ConfigureHotelTable()
                .ConfigureHotelReviewTable()
                .ConfigureRoomTable()
                .ConfigureUserTable();

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

            return modelBuilder;
        }

        private static ModelBuilder ConfigureHotelTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.Name, HotelConstants.MaxNameLength);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.BriefDescription, HotelConstants.MaxBriefDescriptionLength);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.FullDescription, HotelConstants.MaxLengthFullDescription);
            modelBuilder.ConfigureStringLength<HotelTable>(
                hotel => hotel.OwnerName, HotelConstants.MaxNameLength);

            return modelBuilder;
        }

        private static ModelBuilder ConfigureHotelReviewTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureStringLength<HotelReviewTable>(
                review => review.Content, HotelReviewConstants.MaxContentLength);

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
                .UsingEntity(UserRoleTable.Name);

            return modelBuilder;
        }
    }
}
