using System.Linq.Expressions;
using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Extensions;
using HotelBooking.Domain.Models.Hotel;
using HotelBooking.Domain.Models.Room;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelUserRepository"/>
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
            var searchString = hotelSearch.SearchQuery.ToComparableSearchQuery();
            var roomsType = hotelSearch.RoomsType.ToComparableSearchQuery();

            var hotels = _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Rooms)
                .Where(HasTextSimilarity(searchString))
                .AsEnumerable()
                .Where(HasValidRoom(hotelSearch, roomsType))
                .Where(hotel => hotel.Rooms.Count >= hotelSearch.NumberOfRooms);

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

        public async Task<HotelPageDTO> GetHotelPageAsync(Guid id)
        {
            var hotelTable = await _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Discounts)
                .SingleOrDefaultAsync(hotel => hotel.Id == id);

            return _mapper.Map<HotelPageDTO>(hotelTable);
        }

        public IEnumerable<RoomForUserDTO> GetAvailableRooms(
            Guid id, int itemsToSkip, int itemsToTake)
        {
            var rooms = _dbContext.Hotels
                .Where(hotel => hotel.Id == id)
                .SelectMany(hotel => hotel.Rooms)
                .Include(room => room.Bookings)
                .Include(room => room.Hotel.Discounts)
                .AsEnumerable()
                .Where(room => !room.Bookings
                    .Any(booking => booking.IntersectsWith(DateTime.UtcNow)))
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<RoomForUserDTO>>(rooms);
        }

        public int GetAvailableRoomsCount(Guid id)
        {
            return _dbContext.Hotels
                .Where(hotel => hotel.Id == id)
                .SelectMany(hotel => hotel.Rooms)
                .AsEnumerable()
                .Where(room => !room.Bookings
                    .Any(booking => booking.IntersectsWith(DateTime.UtcNow)))
                .Count();
        }

        public IEnumerable<VisitedHotelDTO> GetRecentlyVisitedByPage(
            Guid userId, int itemsToSkip, int itemsToTake)
        {
            var recentlyVisitedHotels = _dbContext.HotelVisits
                .Where(hv => hv.UserId == userId)
                .Include(visit => visit.Hotel)
                .ThenInclude(hotel => hotel.City)
                .GroupBy(visit => visit.HotelId)
                .Select(group => group
                    .OrderByDescending(visit => visit.Date)
                    .FirstOrDefault())
                .AsEnumerable()
                .Where(visit => visit?.Date > HotelVisitConstants.LeastRecentVisitDate)
                .OrderByDescending(visit => visit?.Date)
                .Select(visit => visit?.Hotel)
                .Distinct()
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<VisitedHotelDTO>>(recentlyVisitedHotels);
        }

        public async Task<int> GetVisitedCountAsync(Guid userId)
        {
            var recentlyVisitedHotelsCount = _dbContext.HotelVisits
                .Where(visit => visit.UserId == userId)
                .Include(visit => visit.Hotel)
                .Where(visit => visit.Date > HotelVisitConstants.LeastRecentVisitDate)
                .GroupBy(visit => visit.Hotel)
                .Select(group => group.First());

            if (recentlyVisitedHotelsCount.TryGetNonEnumeratedCount(out int count))
                return count;

            return await recentlyVisitedHotelsCount.CountAsync();
        }
    }
}
