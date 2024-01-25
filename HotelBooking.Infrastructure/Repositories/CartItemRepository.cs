using AutoMapper;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    /// <inheritdoc cref="ICartItemRepository"/>
    internal class CartItemRepository : ICartItemRepository
    {
        private readonly HotelsBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public CartItemRepository(HotelsBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CartItemDTO newCartItem)
        {
            var entityEntry = await _dbContext.CartItems.AddAsync(
                _mapper.Map<CartItemTable>(newCartItem));
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        public Task<bool> ExistsByUserAndRoomAsync(Guid userId, Guid roomId)
        {
            return _dbContext.CartItems.AnyAsync(item =>
                item.UserId == userId &&
                item.RoomId == roomId);
        }

        public IEnumerable<CartItemDTO> GetAllForUserByPage(
            Guid userId, int itemsToSkip, int itemsToTake)
        {
            var items = _dbContext.CartItems
                .Where(item => item.UserId == userId)
                .Skip(itemsToSkip)
                .Take(itemsToTake);

            return _mapper.Map<IEnumerable<CartItemDTO>>(items);
        }

        public async Task<int> GetCountForUserAsync(Guid userId)
        {
            var cartItems = _dbContext.CartItems
                .Where(item => item.UserId == userId);

            if (cartItems.TryGetNonEnumeratedCount(out var count))
                return count;

            return await cartItems.CountAsync();
        }
    }
}
