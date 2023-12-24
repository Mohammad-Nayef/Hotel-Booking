using System.Data.Entity;
using AutoMapper;
using HotelBooking.Db.Tables;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Models;

namespace HotelBooking.Db.Repositories
{
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

        public bool ExistsByUserAndRoomAsync(Guid userId, Guid roomId) =>
            _dbContext.CartItems.Any(item => 
                item.UserId == userId && 
                item.RoomId == roomId);
    }
}
