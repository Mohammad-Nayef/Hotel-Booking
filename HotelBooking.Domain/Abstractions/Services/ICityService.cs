using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface ICityService
    {
        Task<Guid> AddAsync(CityDTO city);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task<IEnumerable<CityForAdminDTO>> GetForAdminByPageAsync(PaginationDTO pagination);
        Task DeleteAsync(Guid id);
        Task<CityDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CityDTO city);
        Task<IEnumerable<CityForAdminDTO>> SearchByCityForAdminByPageAsync(
            PaginationDTO pagination, string searchQuery);
        Task<int> GetSearchByCityForAdminCountAsync(string searchQuery);
    }
}