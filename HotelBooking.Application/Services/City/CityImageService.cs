using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Abstractions.Services.City;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.City
{
    /// <inheritdoc cref="ICityImageService"/>
    internal class CityImageService : ICityImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly ICityImageRepository _cityImageRepository;

        public CityImageService(
            IValidator<IEnumerable<Image>> imagesValidator, 
            ICityImageRepository cityImageRepository)
        {
            _imagesValidator = imagesValidator;
            _cityImageRepository = cityImageRepository;
        }

        public async Task AddAsync(Guid cityId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _cityImageRepository.AddRangeAsync(cityId, images);
        }

        public async Task<FileStream> GetImageAsync(Guid imageId)
        {
            await ValidateCityImageIdAsync(imageId);

            return _cityImageRepository.Get(imageId);
        }

        private async Task ValidateCityImageIdAsync(Guid imageId)
        {
            if (!await _cityImageRepository.ExistsAsync(imageId))
                throw new KeyNotFoundException();
        }

        public async Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId)
        {
            await ValidateCityThumbnailIdAsync(thumbnailId);

            return _cityImageRepository.GetThumbnail(thumbnailId);
        }

        private async Task ValidateCityThumbnailIdAsync(Guid thumbnailId)
        {
            if (!await _cityImageRepository.ExistsAsync(thumbnailId))
                throw new KeyNotFoundException();
        }

        public Task<int> GetCountAsync(Guid cityId) => _cityImageRepository.GetCountAsync(cityId);
    }
}
