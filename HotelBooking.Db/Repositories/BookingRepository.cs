using AutoMapper;
using HotelBooking.Db.Extensions;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Repositories
{
    internal class BookingRepository : IBookingRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookingRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(BookingDTO newBooking)
        {
            await _dbContext.Bookings.AddAsync(_mapper.Map<BookingTable>(newBooking));
            await _dbContext.SaveChangesAsync();
        }

        public bool RoomIsBookedBetween(Guid roomId, DateTime startingDate, DateTime endingDate)
        {
            return _dbContext.Bookings
                .AsEnumerable()
                .Any(booking =>
                    booking.RoomId == roomId &&
                    booking.IntersectsWith(startingDate, endingDate));
        }
    }
}
