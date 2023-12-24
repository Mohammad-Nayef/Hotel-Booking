using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<BookingDTO> _bookingValidator;

        public BookingService(
            IBookingRepository bookingRepository, 
            IValidator<BookingDTO> bookingValidator)
        {
            _bookingRepository = bookingRepository;
            _bookingValidator = bookingValidator;
        }

        public async Task AddAsync(BookingDTO newBooking)
        {
            await _bookingValidator.ValidateAndThrowAsync(newBooking);

            newBooking.CreationDate = DateTime.UtcNow;

            await _bookingRepository.AddAsync(newBooking);
        }
    }
}
