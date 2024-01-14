using HotelBooking.Domain.Models.City;
using System.Linq.Expressions;

namespace HotelBooking.Domain.Abstractions.Repositories.City
{
    public interface ICityAdminRepository
    {
        IEnumerable<CityForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);
        IEnumerable<CityForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression);
    }
}
