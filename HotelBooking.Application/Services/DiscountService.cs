using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    /// <inheritdoc cref="IDiscountService"/>
    internal class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IValidator<DiscountDTO> _discountValidator;

        public DiscountService(
            IDiscountRepository _discountRepository, IValidator<DiscountDTO> _discountValidator)
        {
            this._discountRepository = _discountRepository;
            this._discountValidator = _discountValidator;
        }

        public async Task<Guid> AddAsync(DiscountDTO discount)
        {
            await _discountValidator.ValidateAndThrowAsync(discount);

            return await _discountRepository.AddAsync(discount);
        }
    }
}
