using AutoMapper;
using HotelBooking.Db.Extensions;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models.Hotel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelBooking.Db.Repositories.Hotel
{
    internal class HotelUserRepository : IHotelUserRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelUserRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake)
        {
            var hotels = GetSearchResultHotels(hotelSearch)
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<HotelForUserDTO>>(hotels);
        }

        public int GetSearchForUserCount(HotelSearchDTO hotelSearch)
        {
            var hotels = GetSearchResultHotels(hotelSearch);

            if (hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return hotels.Count();
        }

        private IEnumerable<HotelTable> GetSearchResultHotels(HotelSearchDTO hotelSearch)
        {
            var searchString = hotelSearch.SearchQuery.ToLower();
            var roomsType = hotelSearch.RoomsType.ToLower();

            var hotels = _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Images)
                .Include(hotel => hotel.Rooms)
                .Where(HasTextSimilarity(searchString))
                .AsNoTracking()
                .AsEnumerable()
                .Where(HasValidRoom(hotelSearch, roomsType))
                .Where(hotel => hotel.Rooms.Count() >= hotelSearch.NumberOfRooms);

            return hotels;
        }

        private static Func<HotelTable, bool> HasValidRoom(
            HotelSearchDTO hotelSearch, string roomsType)
        {
            return hotel => hotel.Rooms
                .Exists(room =>
                    room.AdultsCapacity >= hotelSearch.NumberOfAdults &&
                    room.ChildrenCapacity >= hotelSearch.NumberOfChildren &&
                    room.PricePerNight.IsBetweenInclusive(
                        hotelSearch.MinRoomPrice, hotelSearch.MaxRoomPrice) &&
                    room.Type.ToLower().Contains(roomsType) &&
                    IsNotBookedInTheNeededInterval(hotelSearch, room));
        }

        private static bool IsNotBookedInTheNeededInterval(
            HotelSearchDTO hotelSearch, RoomTable room)
        {
            return room.Bookings.TrueForAll(booking =>
                !booking.IntersectsWith(hotelSearch.CheckinDate, hotelSearch.CheckoutDate));
        }

        private static Expression<Func<HotelTable, bool>> HasTextSimilarity(string searchString)
        {
            return hotel =>
                hotel.Name.ToLower().Contains(searchString) ||
                hotel.BriefDescription.ToLower().Contains(searchString) ||
                hotel.Rooms.Any(room =>
                    room.BriefDescription.ToLower().Contains(searchString)) ||
                hotel.City.Name.ToLower().Contains(searchString) ||
                hotel.City.CountryName.ToLower().Contains(searchString);
        }

        public HotelPageDTO GetHotelPage(Guid id)
        {
            var hotelTable = _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Images)
                .Include(hotel => hotel.Discounts)
                .AsNoTracking()
                .SingleOrDefault(hotel => hotel.Id == id);

            return _mapper.Map<HotelPageDTO>(hotelTable);
        }
    }
}
