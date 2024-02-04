using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Abstractions.Services.Room;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class CartItemValidator : AbstractValidator<CartItemDTO>
    {
        public CartItemValidator(
            IUserService userService,
            IRoomService roomService,
            ICartItemRepository cartItemRepository)
        {
            RuleFor(item => item.UserId)
                .NotNull()
                .MustAsync(async (userId, cancellation) =>
                    await userService.ExistsAsync(userId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(item => item.RoomId)
                .NotNull()
                .MustAsync(async (roomId, cancellation) =>
                    await roomService.ExistsAsync(roomId))
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(item => item)
                .MustAsync(async (item, cancellation) =>
                    !await cartItemRepository.ExistsByUserAndRoomAsync(item.UserId, item.RoomId))
                .WithMessage($"The room already exists in the user's cart.")
                .WithName("CartItem");
        }
    }
}
