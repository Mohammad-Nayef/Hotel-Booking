using System.Linq.Expressions;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelAdminRepository
    {
        IEnumerable<HotelForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);
        IEnumerable<HotelForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);
    }
}
