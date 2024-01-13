using System.Linq.Expressions;
using AutoMapper;
using HotelBooking.Db.Extensions;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories
{
    internal class HotelRepository : IHotelRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public HotelRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(HotelDTO newHotel)
        {
            var entityEntry = await _dbContext.Hotels.AddAsync(
                _mapper.Map<HotelTable>(newHotel));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Hotels.Remove(new HotelTable { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) =>
            _dbContext.Hotels.AnyAsync(hotel => hotel.Id == id);

        public async Task<HotelDTO> GetByIdAsync(Guid id) =>
            _mapper.Map<HotelDTO>(await _dbContext.Hotels
                .AsNoTracking()
                .FirstOrDefaultAsync());

        public async Task<int> GetCountAsync()
        {
            if (_dbContext.Hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return await _dbContext.Hotels.CountAsync();
        }

        public IEnumerable<HotelForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake) =>
            _dbContext.HotelsForAdmin
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();

        public IEnumerable<HotelForAdminDTO> SearchByHotelForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking()
                .AsEnumerable();
        }

        public Task<int> GetSearchByHotelForAdminCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .CountAsync();
        }

        public async Task UpdateAsync(HotelDTO hotel)
        {
            _dbContext.Hotels.Update(_mapper.Map<HotelTable>(hotel));
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> GetNumberOfImagesAsync(Guid hotelId) =>
            _dbContext.Images.Where(image => image.HotelId == hotelId).CountAsync();

        public IEnumerable<FeaturedHotelDTO> GetHotelsWithActiveDiscountsByPage(
            int itemsToSkip, int itemsToTake)
        {
            var hotels = GetHotelsWithActiveDiscounts()
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<FeaturedHotelDTO>>(hotels);
        }

        private IEnumerable<HotelTable> GetHotelsWithActiveDiscounts()
        {
            return _dbContext.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Discounts)
                .Include(hotel => hotel.Images)
                .AsEnumerable()
                .Where(hotel => hotel.Discounts.HasActiveDiscount());
        }

        public int GetHotelsWithActiveDiscountsCount()
        {
            var hotels = GetHotelsWithActiveDiscounts();

            if (hotels.TryGetNonEnumeratedCount(out var count))
                return count;

            return hotels.Count();
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
