using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IHotelRepository
    {
        Task<Guid> AddAsync(HotelDTO newHotel);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        IEnumerable<HotelForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake);
        Task DeleteAsync(Guid id);
    }
}
