﻿using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Services.Hotel;
using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Application.Services.Hotel
{
    internal class HotelUserService : IHotelUserService
    {
        private readonly IValidator<PaginationDTO> _paginationValidator;
        private readonly IValidator<HotelSearchDTO> _hotelSearchValidator;
        private readonly IHotelService _hotelService;
        private readonly IHotelReviewService _hotelReviewService;
        private readonly IHotelUserRepository _hotelUserRepository;
        private readonly IHotelDiscountRepository _hotelDiscountRepository;

        public HotelUserService(
            IValidator<PaginationDTO> paginationValidator,
            IValidator<HotelSearchDTO> hotelSearchValidator,
            IHotelService hotelService,
            IHotelReviewService hotelReviewService,
            IHotelUserRepository hotelUserRepository,
            IHotelDiscountRepository hotelDiscountRepository)
        {
            _paginationValidator = paginationValidator;
            _hotelSearchValidator = hotelSearchValidator;
            _hotelService = hotelService;
            _hotelReviewService = hotelReviewService;
            _hotelUserRepository = hotelUserRepository;
            _hotelDiscountRepository = hotelDiscountRepository;
        }

        public async Task<IEnumerable<FeaturedHotelDTO>> GetFeaturedHotelsByPageAsync(
            PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);

            return _hotelDiscountRepository.GetHotelsWithActiveDiscountsByPage(
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public int GetFeaturedHotelsCount() =>
            _hotelDiscountRepository.GetHotelsWithActiveDiscountsCount();

        public async Task<IEnumerable<HotelForUserDTO>> SearchByPageAsync(
            HotelSearchDTO hotelSearch, PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);
            await _hotelSearchValidator.ValidateAndThrowAsync(hotelSearch);

            return _hotelUserRepository.SearchForUserByPage(
                hotelSearch,
                (pagination.PageNumber - 1) * pagination.PageSize,
                pagination.PageSize);
        }

        public int GetSearchCount(HotelSearchDTO hotelSearch) =>
            _hotelUserRepository.GetSearchForUserCount(hotelSearch);

        public async Task<HotelPageDTO> GetHotelPageAsync(Guid id)
        {
            await _hotelService.ValidateIdAsync(id);

            return _hotelUserRepository.GetHotelPage(id);
        }

        public async Task<IEnumerable<ReviewForHotelPageDTO>> GetReviewsByPageAsync(
            Guid id, PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);
            await _hotelService.ValidateIdAsync(id);

            return _hotelReviewService.GetReviewsByHotelByPage(id, pagination);
        }

        public Task<int> GetReviewsCountAsync(Guid id) =>
            _hotelReviewService.GetReviewsByHotelCountAsync(id);
    }
}
