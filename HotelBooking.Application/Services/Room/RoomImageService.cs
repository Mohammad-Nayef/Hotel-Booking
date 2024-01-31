using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Room
{
    /// <inheritdoc cref="IRoomImageService"/>
    internal class RoomImageService : ImageService, IRoomImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly IImageRepository _imageRepository;
        private readonly IRoomService _roomService;

        public RoomImageService(
            IValidator<IEnumerable<Image>> imagesValidator,
            IImageRepository imageRepository,
            IRoomService roomService,
            IValidator<ImageSizeDTO> imageSizeValidator)
            : base(imageRepository, imageSizeValidator)
        {
            _imagesValidator = imagesValidator;
            _imageRepository = imageRepository;
            _roomService = roomService;
        }

        public override async Task AddAsync(Guid entityId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _roomService.ValidateIdAsync(entityId);
            await _imageRepository.AddRangeAsync(entityId, images);
        }

        public override async Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId)
        {
            await _roomService.ValidateIdAsync(entityId);

            return _imageRepository.GetImagesIds(entityId);
        }
    }
}
