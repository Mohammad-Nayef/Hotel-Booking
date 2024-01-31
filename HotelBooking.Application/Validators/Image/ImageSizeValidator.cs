using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.Image;

namespace HotelBooking.Application.Validators.Image
{
    internal class ImageSizeValidator : AbstractValidator<ImageSizeDTO>
    {

        public ImageSizeValidator()
        {
            RuleFor(size => size.Width)
                .NotNull()
                .InclusiveBetween(0, ImagesConstants.MaxWidth);

            RuleFor(size => size.Height)
                .NotNull()
                .InclusiveBetween(0, ImagesConstants.MaxHeight);

            RuleFor(size => size)
                .Must(size => !(size.Width == 0 && size.Height == 0))
                .WithMessage("Width and height can't be 0 together.");
        }
    }
}
