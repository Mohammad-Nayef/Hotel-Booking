using FluentValidation;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class RoomValidator : AbstractValidator<RoomDTO>
    {
        public RoomValidator(IHotelService hotelService)
        {
            RuleFor(room => room.Number)
                .NotNull();

            RuleFor(room => room.Type)
                .NotNull()
                .Length(RoomConstants.MinTypeLength, RoomConstants.MaxTypeLength);

            RuleFor(room => room.AdultsCapacity)
                .NotNull()
                .InclusiveBetween(
                    RoomConstants.MinAdultsCapacity, RoomConstants.MaxAdultsCapacity);

            RuleFor(room => room.ChildrenCapacity)
                .NotNull()
                .InclusiveBetween(
                    RoomConstants.MinChildrenCapacity, RoomConstants.MaxChildrenCapacity);

            RuleFor(room => room.BriefDescription)
                .NotNull()
                .Length(
                    RoomConstants.MinBriefDescriptionLength,
                    RoomConstants.MaxBriefDescriptionLength);

            RuleFor(room => room.PricePerNight)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
