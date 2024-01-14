using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Hotel
{
    internal class HotelImageService : IHotelImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly IHotelImageRepository _hotelImageRepository;

        public HotelImageService(
            IValidator<IEnumerable<Image>> imagesValidator, 
            IHotelImageRepository hotelImageRepository)
        {
            _imagesValidator = imagesValidator;
            _hotelImageRepository = hotelImageRepository;
        }

        public async Task AddAsync(Guid hotelId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _hotelImageRepository.AddRangeAsync(hotelId, images);
        }

        public async Task<FileStream> GetImageAsync(Guid imageId)
        {
            await ValidateHotelImageIdAsync(imageId);

            return _hotelImageRepository.Get(imageId);
        }

        private async Task ValidateHotelImageIdAsync(Guid imageId)
        {
            if (!await _hotelImageRepository.ExistsAsync(imageId))
                throw new KeyNotFoundException();
        }

        public async Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId)
        {
            await ValidateHotelThumbnailIdAsync(thumbnailId);

            return _hotelImageRepository.GetThumbnail(thumbnailId);
        }

        private async Task ValidateHotelThumbnailIdAsync(Guid thumbnailId)
        {
            if (!await _hotelImageRepository.ThumbnailExistsAsync(thumbnailId))
                throw new KeyNotFoundException();
        }

        public Task<int> GetCountAsync(Guid hotelId) => 
            _hotelImageRepository.GetCountAsync(hotelId);
    }
}
