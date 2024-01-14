using System.Linq.Expressions;
using HotelBooking.Domain.Models.Room;

namespace HotelBooking.Domain.Abstractions.Repositories.Room
{
    public interface IRoomAdminRepository
    {
        IEnumerable<RoomForAdminDTO> GetByPage(int itemsToSkip, int itemsToTake);
        IEnumerable<RoomForAdminDTO> SearchByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchCountAsync(
            Expression<Func<RoomForAdminDTO, bool>> searchExpression);
    }
}
