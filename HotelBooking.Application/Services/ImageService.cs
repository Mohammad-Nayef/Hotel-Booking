using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services
{
    internal abstract class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public abstract Task AddAsync(Guid entityId, IEnumerable<Image> images);

        public async Task<FileStream> GetImageAsync(Guid imageId)
        {
            await ValidateImageIdAsync(imageId);

            return _imageRepository.Get(imageId);
        }

        private async Task ValidateImageIdAsync(Guid imageId)
        {
            if (!await _imageRepository.ExistsAsync(imageId))
                throw new KeyNotFoundException();
        }

        public async Task<FileStream> GetThumbnailOfImageAsync(Guid thumbnailId)
        {
            await ValidateThumbnailIdAsync(thumbnailId);

            return _imageRepository.GetThumbnail(thumbnailId);
        }

        private async Task ValidateThumbnailIdAsync(Guid thumbnailId)
        {
            if (!await _imageRepository.ExistsAsync(thumbnailId))
                throw new KeyNotFoundException();
        }

        public Task<int> GetCountAsync(Guid entityId) => _imageRepository.GetCountAsync(entityId);

        public abstract Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId);
    }
}
