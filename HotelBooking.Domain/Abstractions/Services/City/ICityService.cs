using HotelBooking.Domain.Models.City;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    public interface ICityService
    {
        Task<Guid> AddAsync(CityDTO city);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<CityDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CityDTO city);
        Task ValidateIdAsync(Guid id);
    }
}