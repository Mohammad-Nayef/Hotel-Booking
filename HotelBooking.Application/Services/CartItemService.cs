using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    internal class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IValidator<CartItemDTO> _cartItemValidator;

        public CartItemService(
            ICartItemRepository cartItemRepository, IValidator<CartItemDTO> cartItemValidator)
        {
            _cartItemRepository = cartItemRepository;
            _cartItemValidator = cartItemValidator;
        }

        public async Task<Guid> AddAsync(CartItemDTO newCartItem)
        {
            await _cartItemValidator.ValidateAndThrowAsync(newCartItem);

            newCartItem.AddingDate = DateTime.UtcNow;

            return await _cartItemRepository.AddAsync(newCartItem);
        }
    }
}
