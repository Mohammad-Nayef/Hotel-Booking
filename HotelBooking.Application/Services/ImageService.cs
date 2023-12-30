using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services
{
    internal class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;

        public ImageService(
            IImageRepository imageRepository, IValidator<IEnumerable<Image>> imagesValidator)
        {
            _imageRepository = imageRepository;
            _imagesValidator = imagesValidator;
        }

        public async Task AddForCityAsync(Guid cityId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _imageRepository.AddForCityAsync(cityId, images);
        }

        public async Task AddForHotelAsync(Guid hotelId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _imageRepository.AddForHotelAsync(hotelId, images);
        }

        public async Task AddForRoomAsync(Guid roomId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _imageRepository.AddForRoomAsync(roomId, images);
        }
    }
}
