﻿using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class HotelReviewService : IHotelReviewService
    {
        private readonly IHotelReviewRepository _hotelReviewRepository;
        private readonly IValidator<HotelReviewDTO> _hotelReviewValidator;

        public HotelReviewService(
            IHotelReviewRepository hotelReviewRepository,
            IValidator<HotelReviewDTO> hotelReviewValidator)
        {
            _hotelReviewRepository = hotelReviewRepository;
            _hotelReviewValidator = hotelReviewValidator;
        }

        public async Task AddAsync(HotelReviewDTO newReview)
        {
            await _hotelReviewValidator.ValidateAndThrowAsync(newReview);

            newReview.CreationDate = DateTime.UtcNow;

            await _hotelReviewRepository.AddAsync(newReview);
        }
    }
}
