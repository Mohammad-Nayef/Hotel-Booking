using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Constants;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Validators
{
    internal class ImagesValidator : AbstractValidator<IEnumerable<Image>>
    {
        public ImagesValidator(ICityRepository cityRepository)
        {
            RuleFor(images => images)
                .Must(images => images.Count() <= ImagesConstants.MaxNumberOfImagesPerRequest)
                .WithMessage(
                    $"Number of images must not exceed {ImagesConstants.MaxNumberOfImagesPerRequest}")
                .Must(images => images.All(image => IsValidWidth(image.Width)))
                .WithMessage(
                    $"Images width must be between {ImagesConstants.MinWidth} and " +
                    $"{ImagesConstants.MaxWidth} inclusive.")
                .Must(images => images.All(image => IsValidHeight(image.Height)))
                .WithMessage(
                    $"Images height must be between {ImagesConstants.MinHeight} and " +
                    $"{ImagesConstants.MaxHeight} inclusive.")
                .WithName("Images");
        }
        
        private bool IsValidHeight(int height) =>
            height >= ImagesConstants.MinHeight && height <= ImagesConstants.MaxHeight;

        private bool IsValidWidth(int width) => 
            width >= ImagesConstants.MinWidth && width <= ImagesConstants.MaxWidth;
    }
}
