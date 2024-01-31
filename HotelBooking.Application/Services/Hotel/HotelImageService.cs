using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Hotel
{
    /// <inheritdoc cref="IHotelImageService"/>
    internal class HotelImageService : ImageService, IHotelImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly IImageRepository _imageRepository;
        private readonly IHotelService _hotelService;

        public HotelImageService(
            IValidator<IEnumerable<Image>> imagesValidator,
            IImageRepository imageRepository,
            IHotelService hotelService,
            IValidator<ImageSizeDTO> imageSizeValidator)
            : base(imageRepository, imageSizeValidator)
        {
            _imagesValidator = imagesValidator;
            _imageRepository = imageRepository;
            _hotelService = hotelService;
        }

        public override async Task AddAsync(Guid entityId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _hotelService.ValidateIdAsync(entityId);
            await _imageRepository.AddRangeAsync(entityId, images);
        }

        public override async Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId)
        {
            await _hotelService.ValidateIdAsync(entityId);

            return _imageRepository.GetImagesIds(entityId);
        }
    }
}
