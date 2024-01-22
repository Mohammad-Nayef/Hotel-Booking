using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services
{
    /// <inheritdoc cref="ICartItemService"/>
    internal class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IValidator<CartItemDTO> _cartItemValidator;
        private readonly IUserService _userService;
        private readonly IValidator<PaginationDTO> _paginationValidator;

        public CartItemService(
            ICartItemRepository cartItemRepository,
            IValidator<CartItemDTO> cartItemValidator,
            IUserService userService,
            IValidator<PaginationDTO> paginationValidator)
        {
            _cartItemRepository = cartItemRepository;
            _cartItemValidator = cartItemValidator;
            _userService = userService;
            _paginationValidator = paginationValidator;
        }

        public async Task<Guid> AddAsync(CartItemDTO newCartItem)
        {
            await _cartItemValidator.ValidateAndThrowAsync(newCartItem);

            newCartItem.AddingDate = DateTime.UtcNow;

            return await _cartItemRepository.AddAsync(newCartItem);
        }

        public async Task<IEnumerable<CartItemDTO>> GetAllForUserByPageAsync(
            Guid userId, PaginationDTO pagination)
        {
            await _paginationValidator.ValidateAndThrowAsync(pagination);
            await ValidateUserIdAsync(userId);

            return _cartItemRepository.GetAllForUserByPage(
                userId, (pagination.PageNumber - 1) * pagination.PageSize, pagination.PageSize);
        }

        private async Task ValidateUserIdAsync(Guid userId)
        {
            if (!await _userService.ExistsAsync(userId))
                throw new KeyNotFoundException();
        }

        public async Task<int> GetCountForUserAsync(Guid userId)
        {
            await ValidateUserIdAsync(userId);

            return await _cartItemRepository.GetCountForUserAsync(userId);
        }
    }
}
