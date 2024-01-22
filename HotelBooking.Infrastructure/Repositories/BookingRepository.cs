using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Extensions;
using HotelBooking.Infrastructure.Tables;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="IBookingRepository"/>
    internal class BookingRepository : IBookingRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(
            HotelsBookingDbContext dbContext, IMapper mapper, ILogger<BookingRepository> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddAsync(BookingDTO newBooking)
        {
            var booking = _mapper.Map<BookingTable>(newBooking);
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Created booking with Id: {id}", booking.Id);
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
