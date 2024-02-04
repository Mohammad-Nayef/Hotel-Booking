using FluentValidation;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class DiscountValidator : AbstractValidator<DiscountDTO>
    {
        public DiscountValidator(IHotelService hotelService)
        {
            RuleFor(discount => discount.Reason)
                .Length(DiscountConstants.MinReasonLength, DiscountConstants.MaxReasonLength);

            RuleFor(discount => discount.AmountPercent)
                .InclusiveBetween(
                    DiscountConstants.MinAmountPercent, DiscountConstants.MaxAmountPercent);

            RuleFor(discount => discount.HotelId)
                .MustAsync(async (hotelId, cancellation) =>
                    await hotelService.ExistsAsync(hotelId))
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
