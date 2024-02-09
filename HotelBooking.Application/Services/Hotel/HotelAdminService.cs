using System.Linq.Expressions;
using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Extensions;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using SixLabors.ImageSharp;

namespace HotelBooking.Application.Services.Hotel
{
    /// <inheritdoc cref="IHotelAdminService"/>
    internal class HotelAdminService : IHotelAdminService
    {
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IHotelService _hotelService;
        private readonly IHotelImageService _hotelImageService;
        private readonly IHotelAdminRepository _hotelAdminRepository;

        public HotelAdminService(
            IValidator<PaginationDTO> paginationValidator,
            IHotelService hotelService,
            IHotelImageService hotelImageService,
            IHotelAdminRepository hotelAdminRepository)
        {
            _paginationValidator = paginationValidator;
            _hotelService = hotelService;
            _hotelImageService = hotelImageService;
            _hotelAdminRepository = hotelAdminRepository;
        }

        public async Task<IEnumerable<HotelForAdminDTO>> GetByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelAdminRepository.GetByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public async Task<IEnumerable<HotelForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelAdminRepository.SearchByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize,
                ToAdminSearchExpression(searchQuery));
        }

        private Expression<Func<HotelForAdminDTO, bool>> ToAdminSearchExpression(
            string searchQuery)
        {
            searchQuery = searchQuery.ToComparableSearchQuery();

            return hotel =>
                hotel.Name.ToLower().Contains(searchQuery) ||
                hotel.OwnerName.ToLower().Contains(searchQuery);
        }

        public Task<int> GetSearchCountAsync(string searchQuery)
        {
            return _hotelAdminRepository.GetSearchCountAsync(
                ToAdminSearchExpression(searchQuery));
        }

        public async Task AddImagesAsync(Guid hotelId, IEnumerable<Image> images)
        {
            await _hotelService.ValidateIdAsync(hotelId);
            await ValidateNumberOfImagesForHotelAsync(hotelId, images.Count());

            await _hotelImageService.AddAsync(hotelId, images);
        }

        private async Task ValidateNumberOfImagesForHotelAsync(
            Guid hotelId, int numberOfImagesToAdd)
        {
            var numberOfStoredImages = await _hotelImageService.GetCountAsync(hotelId);

            if (numberOfStoredImages + numberOfImagesToAdd >
                    ImagesConstants.MaxNumberOfImagesPerEntity)
                throw new EntityImagesLimitExceededException();
        }
    }
}
