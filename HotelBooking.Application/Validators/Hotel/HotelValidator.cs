using FluentValidation;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Application.Validators.Hotel
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
                    HotelConstants.MinFullDescriptionLength,
                    HotelConstants.MaxFullDescriptionLength);

            RuleFor(hotel => hotel.StarRating)
                .NotNull()
                .InclusiveBetween(HotelConstants.MinStarRating, HotelConstants.MaxStarRating);

            RuleFor(hotel => hotel.Geolocation)
                .NotNull()
                .Matches(HotelConstants.GeolocationRegex)
                .WithMessage("{PropertyName} has invalid format.");

            RuleFor(hotel => hotel.CityId)
                .MustAsync(async (cityId, cancellation) => await cityService.ExistsAsync(cityId))
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
