using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class HotelReviewValidator : AbstractValidator<HotelReviewDTO>
    {
        public HotelReviewValidator(
            IUserService userService,
            IHotelRepository hotelRepository,
            IHotelReviewRepository hotelReviewRepository)
        {
            RuleFor(review => review.Content)
                .NotNull()
                .Length(
                    HotelReviewConstants.MinContentLength, HotelReviewConstants.MaxContentLength);

            RuleFor(review => review.UserId)
                .NotNull()
                .MustAsync(async (userId, cancellation) =>
                    await userService.ExistsAsync(userId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(review => review.HotelId)
                .NotNull()
                .MustAsync(async (hotelId, cancellation) =>
                    await hotelRepository.ExistsAsync(hotelId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(review => review)
                .MustAsync(async (review, cancellation) =>
                    !await hotelReviewRepository.ExistsByUserAndHotelAsync(
                        review.UserId, review.HotelId))
                .WithMessage($"The user has already submitted a review for this hotel.")
                .WithName("HotelReview");
        }
    }
}

