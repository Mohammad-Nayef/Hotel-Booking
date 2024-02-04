using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.City
{
    /// <inheritdoc cref="ICityImageService"/>
    internal class CityImageService : ImageService, ICityImageService
    {
        private readonly IValidator<IEnumerable<Image>> _imagesValidator;
        private readonly IImageRepository _imageRepository;
        private readonly ICityService _cityService;

        public CityImageService(
            IValidator<IEnumerable<Image>> imagesValidator,
            IImageRepository imageRepository,
            ICityService cityService,
            IValidator<ImageSizeDTO> imageSizeValidator)
            : base(imageRepository, imageSizeValidator)
        {
            _imagesValidator = imagesValidator;
            _imageRepository = imageRepository;
            _cityService = cityService;
        }

        public override async Task AddAsync(Guid entityId, IEnumerable<Image> images)
        {
            await _imagesValidator.ValidateAndThrowAsync(images);
            await _cityService.ValidateIdAsync(entityId);
            await _imageRepository.AddRangeAsync(entityId, images);
        }

        public override async Task<IEnumerable<Guid>> GetImagesIdsAsync(Guid entityId)
        {
            await _cityService.ValidateIdAsync(entityId);

            return _imageRepository.GetImagesIds(entityId);
        }
    }
}
