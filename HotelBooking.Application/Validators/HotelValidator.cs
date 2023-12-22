using FluentValidation;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class HotelValidator : AbstractValidator<HotelDTO>
    {
        public HotelValidator(ICityService cityService)
        {
            RuleFor(hotel => hotel.Name)
                .NotNull()
                .Length(HotelConstants.MinNameLength, HotelConstants.MaxNameLength);

            RuleFor(hotel => hotel.BriefDescription)
                .NotNull()
                .Length(
                    HotelConstants.MinBriefDescriptionLength,
                    HotelConstants.MaxBriefDescriptionLength);

            RuleFor(hotel => hotel.BriefDescription)
                .NotNull()
                .NotEmpty()
                .Length(
                    HotelConstants.MinLengthFullDescription,
                    HotelConstants.MaxLengthFullDescription);

            RuleFor(hotel => hotel.StarRating)
                .NotNull()
                .InclusiveBetween(1, 5);

            RuleFor(hotel => hotel.Geolocation)
                .NotNull()
                .Matches(HotelConstants.GeolocationRegex)
                .WithMessage("{PropertyName} has invalid format.");

            RuleFor(hotel => hotel.CityId)
                .MustAsync((cityId, cancellation) => cityService.ExistsAsync(cityId))
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
