using System.Linq.Expressions;
using HotelBooking.Domain.Abstractions.Repositories.Room;
using HotelBooking.Domain.Models.Room;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories.Room
{
    internal class RoomAdminRepository : IRoomAdminRepository
    {

        private readonly HotelsBookingDbContext _dbContext;

        public RoomAdminRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<RoomForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.RoomsForAdmin
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking();
        }

        public IEnumerable<RoomForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<RoomForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.RoomsForAdmin
                .Where(searchExpression)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking();
        }

        public Task<int> GetSearchCountAsync(
            Expression<Func<RoomForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.RoomsForAdmin
                .Where(searchExpression)
                .CountAsync();
        }
    }
}
