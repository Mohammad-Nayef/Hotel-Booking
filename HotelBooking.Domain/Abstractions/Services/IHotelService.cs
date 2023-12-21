using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IHotelService
    {
        Task<Guid> AddAsync(HotelDTO hotel);
        Task<bool> ExistsAsync(Guid Id);
        Task<int> GetCountAsync();
        Task<IEnumerable<HotelForAdminDTO>> GetForAdminByPageAsync(PaginationDTO pagination);
        Task DeleteAsync(Guid id);
    }
}
