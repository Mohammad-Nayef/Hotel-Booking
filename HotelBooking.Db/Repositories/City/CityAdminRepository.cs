using System.Linq.Expressions;
using HotelBooking.Domain.Abstractions.Repositories.City;
using HotelBooking.Domain.Models.City;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Db.Repositories.City
{
    internal class CityAdminRepository : ICityAdminRepository
    {
        private readonly HotelsBookingDbContext _dbContext;

        public CityAdminRepository(HotelsBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CityForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake)
        {
            return _dbContext.CitiesForAdmin
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking();
        }

        public IEnumerable<CityForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .OrderBy(city => city.Name)
                .Skip(itemsToSkip)
                .Take(itemsToTake)
                .AsNoTracking();
        }

        public Task<int> GetSearchCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression)
        {
            return _dbContext.CitiesForAdmin
                .Where(searchExpression)
                .CountAsync();
        }
    }
}
