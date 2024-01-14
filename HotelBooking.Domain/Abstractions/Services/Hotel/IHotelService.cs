using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    public interface IHotelService
    {
        Task<Guid> AddAsync(HotelDTO hotel);
        Task<bool> ExistsAsync(Guid Id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<HotelDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(HotelDTO hotel);
        Task ValidateIdAsync(Guid id);
    }
}
