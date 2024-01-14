using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Application.Validators
{
    internal class CityValidator : AbstractValidator<CityDTO>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name)
                .NotNull()
                .Length(CityConstants.MinNameLength, CityConstants.MaxNameLength);

            RuleFor(city => city.CountryName)
                .NotNull()
                .Length(CityConstants.MinCountryNameLength, CityConstants.MaxCountryNameLength);

            RuleFor(city => city.PostOffice)
                .NotNull()
                .Length(CityConstants.MinPostOfficeLength, CityConstants.MaxPostOfficeLength);
        }
    }
}
