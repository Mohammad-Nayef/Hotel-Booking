using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<BookingDTO> _bookingValidator;
        private readonly IHotelDiscountRepository _hotelDiscountRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(
            IBookingRepository bookingRepository, 
            IValidator<BookingDTO> bookingValidator,
            IHotelDiscountRepository hotelDiscountRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingValidator = bookingValidator;
            _hotelDiscountRepository = hotelDiscountRepository;
            _roomRepository = roomRepository;
        }

        public async Task AddAsync(BookingDTO newBooking)
        {
            await _bookingValidator.ValidateAndThrowAsync(newBooking);

            newBooking.CreationDate = DateTime.UtcNow;
            await AddPriceAsync(newBooking);

            await _bookingRepository.AddAsync(newBooking);
        }

        private async Task AddPriceAsync(BookingDTO newBooking)
        {
            var room = await _roomRepository.GetByIdAsync(newBooking.RoomId);
            var hotelDiscountPercentage = 
                (await _hotelDiscountRepository.GetHighestActiveDiscountAsync(room.HotelId))?
                .AmountPercent ?? 0;
            var originalPrice = 
                ((newBooking.EndingDate - newBooking.StartingDate).Days + 1) * room.PricePerNight;
            newBooking.Price = 
                originalPrice - originalPrice * (decimal)(hotelDiscountPercentage / 100);
        }
    }
}
