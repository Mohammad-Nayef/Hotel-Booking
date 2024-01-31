using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services
{
    internal abstract class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IValidator<ImageSizeDTO> _imageSizeValidator;

        public ImageService(
            IImageRepository imageRepository, IValidator<ImageSizeDTO> imageSizeValidator)
        {
            _imageRepository = imageRepository;
            _imageSizeValidator = imageSizeValidator;
        }

        public abstract Task AddAsync(Guid entityId, IEnumerable<Image> images);

        public async Task<Stream> GetImageAsync(Guid imageId, ImageSizeDTO imageSize)
        {
            await _imageSizeValidator.ValidateAndThrowAsync(imageSize);
            await ValidateImageIdAsync(imageId);

            return await _imageRepository.GetAsync(imageId, imageSize);
        }

        private async Task ValidateImageIdAsync(Guid imageId)
        {
            if (!await _imageRepository.ExistsAsync(imageId))
                throw new KeyNotFoundException();
        }

        public Task<int> GetCountAsync(Guid entityId) => _imageRepository.GetCountAsync(entityId);

        public abstract Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId);
    }
}
