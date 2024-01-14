using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Abstractions.Services.Room;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Room
{
    internal class RoomImageService : IRoomImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly IRoomImageRepository _roomImageRepository;

        public RoomImageService(
            IValidator<IEnumerable<Image>> imagesValidator, 
            IRoomImageRepository roomImageRepository)
        {
            _imagesValidator = imagesValidator;
            _roomImageRepository = roomImageRepository;
        }

        public async Task AddAsync(Guid roomId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _roomImageRepository.AddRangeAsync(roomId, images);
        }

        public async Task<FileStream> GetImageAsync(Guid imageId)
        {
            await ValidateRoomImageIdAsync(imageId);

            return _roomImageRepository.Get(imageId);
        }

        public async Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId)
        {
            await ValidateRoomThumbnailIdAsync(thumbnailId);

            return _roomImageRepository.GetThumbnail(thumbnailId);
        }

        private async Task ValidateRoomThumbnailIdAsync(Guid thumbnailId)
        {
            if (!await _roomImageRepository.ThumbnailExistsAsync(thumbnailId))
                throw new KeyNotFoundException();
        }

        private async Task ValidateRoomImageIdAsync(Guid imageId)
        {
            if (!await _roomImageRepository.ExistsAsync(imageId))
                throw new KeyNotFoundException();
        }

        public Task<int> GetCountAsync(Guid roomId) => _roomImageRepository.GetCountAsync(roomId);
    }
}
