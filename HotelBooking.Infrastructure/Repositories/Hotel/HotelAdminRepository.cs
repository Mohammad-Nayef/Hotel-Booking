using System.Linq.Expressions;
using HotelBooking.Domain.Abstractions.Repositories.Hotel;
using HotelBooking.Domain.Models.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories.Hotel
{
    /// <inheritdoc cref="IHotelAdminRepository"/>
    internal class HotelAdminRepository : IHotelAdminRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public HotelAdminRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<HotelForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.HotelsForAdmin
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake);
        }

        public IEnumerable<HotelForAdminDTO> SearchByPage(
        int itemsToSkip,
        int itemsToTake,
        Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .OrderBy(hotel => hotel.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake);
        }

        public Task<int> GetSearchCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.HotelsForAdmin
                .Where(searchExpression)
                .CountAsync();
        }
    }
}
