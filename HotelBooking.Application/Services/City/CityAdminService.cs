using System.Linq.Expressions;
using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Abstractions.Services.City;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Extensions;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.City
{
    /// <inheritdoc cref="ICityAdminService"/>
    internal class CityAdminService : ICityAdminService
    {
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly ICityService _cityService;
        private readonly ICityImageService _cityImageService;
        private readonly ICityAdminRepository _cityAdminRepository;

        public CityAdminService(
            IValidator<PaginationDTO> paginationValidator,
            ICityService cityService,
            ICityImageService cityImageService,
            ICityAdminRepository cityAdminRepository)
        {
            _paginationValidator = paginationValidator;
            _cityService = cityService;
            _cityImageService = cityImageService;
            _cityAdminRepository = cityAdminRepository;
        }

        public async Task<IEnumerable<CityForAdminDTO>> GetByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _cityAdminRepository.GetByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task<IEnumerable<CityForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _cityAdminRepository.SearchByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize,
                ToSearchExpression(searchQuery));
        }

        private Expression<Func<CityForAdminDTO, bool>> ToSearchExpression(string searchQuery)
        {
            searchQuery = searchQuery.ToComparableSearchQuery();

            return city =>
                city.Name.ToLower().Contains(searchQuery) ||
                city.CountryName.ToLower().Contains(searchQuery);
        }

        public Task<int> GetSearchCountAsync(string searchQuery) =>
            _cityAdminRepository.GetSearchCountAsync(ToSearchExpression(searchQuery));

        public async Task AddImagesAsync(Guid cityId, IEnumerable<Image> images)
        {
            await _cityService.ValidateIdAsync(cityId);
            await ValidateNumberOfImagesForCityAsync(cityId, images.Count());

            await _cityImageService.AddAsync(cityId, images);
        }

        private async Task ValidateNumberOfImagesForCityAsync(Guid cityId, int numberOfImagesToAdd)
        {
            var numberOfStoredImages = await _cityImageService.GetCountAsync(cityId);

            if (numberOfStoredImages + numberOfImagesToAdd >
                    ImagesConstants.MaxNumberOfImagesPerEntity)
                throw new EntityImagesLimitExceededException();
        }
    }
}
