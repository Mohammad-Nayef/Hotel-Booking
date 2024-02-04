using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class BookingValidator : AbstractValidator<BookingDTO>
    {
        public BookingValidator(
            IUserService userService,
            IRoomService roomService,
            IBookingRepository bookingRepository)
        {
            RuleFor(booking => booking.UserId)
                .NotNull()
                .MustAsync(async (userId, cancellation) =>
                    await userService.ExistsAsync(userId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(booking => booking.RoomId)
                .NotNull()
                .MustAsync(async (roomId, cancellation) =>
                    await roomService.ExistsAsync(roomId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(booking => booking)
                .Must(booking => booking.StartingDate < booking.EndingDate)
                .WithMessage("The starting date must be before the ending date.")
                .WithName("Booking");

            RuleFor(booking => booking)
                .Must(booking => !bookingRepository.RoomIsBookedBetween(
                        booking.RoomId, booking.StartingDate, booking.EndingDate))
                .WithMessage("The room is already booked in the given interval.")
                .WithName("Booking");

            RuleFor(booking => booking.StartingDate)
                .Must(startingDate => startingDate >= DateTime.Today)
                .WithMessage("{PropertyName} can't be in the past");
        }
    }
}
