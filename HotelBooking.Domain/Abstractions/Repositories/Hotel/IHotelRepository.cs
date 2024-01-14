using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelRepository
    {
        Task<Guid> AddAsync(HotelDTO newHotel);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<HotelDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(HotelDTO hotel);
    }
}
