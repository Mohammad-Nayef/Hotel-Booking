using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Application.Validators.Hotel
{
    internal class HotelSearchValidator : AbstractValidator<HotelSearchDTO>
    {
        public HotelSearchValidator()
        {
            RuleFor(hotel => hotel.SearchQuery)
                .NotNull()
                .Length(
                    HotelSearchConstants.MinSearchQueryLength,
                    HotelSearchConstants.MaxSearchQueryLength);

            RuleFor(hotel => hotel.CheckinDate)
                .NotNull()
                .Must(checkinDate => checkinDate >= DateTime.Today)
                .WithMessage("{PropertyName} can't be in the past.");

            RuleFor(hotel => hotel.CheckoutDate)
                .NotNull()
                .Must(CheckoutDate => CheckoutDate > DateTime.UtcNow)
                .WithMessage("{PropertyName} must be in the future.");

            RuleFor(hotel => hotel)
                .Must(hotel => hotel.CheckoutDate > hotel.CheckinDate)
                .WithMessage("Checkout date must be after checkin date.");

            RuleFor(hotel => hotel.NumberOfAdults)
                .NotNull()
                .InclusiveBetween(
                    RoomConstants.MinAdultsCapacity, RoomConstants.MaxAdultsCapacity);

            RuleFor(hotel => hotel.NumberOfChildren)
                .NotNull()
                .InclusiveBetween(
                    RoomConstants.MinChildrenCapacity, RoomConstants.MaxChildrenCapacity);

            RuleFor(hotel => hotel.NumberOfRooms)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(hotel => hotel.MinRoomPrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(hotel => hotel.MaxRoomPrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(hotel => hotel.RoomsType)
                .MaximumLength(RoomConstants.MaxTypeLength);

            RuleFor(hotel => hotel)
                .Must(hotel => hotel.MinRoomPrice <= hotel.MaxRoomPrice)
                .WithMessage("Minimum room price must be less than or equal the maximum price.");
        }
    }
}