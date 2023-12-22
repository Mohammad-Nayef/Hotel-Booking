using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface ICityRepository
    {
        Task<Guid> AddAsync(CityDTO city);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        IEnumerable<CityForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake);
        Task<CityDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CityDTO city);
    }
}
