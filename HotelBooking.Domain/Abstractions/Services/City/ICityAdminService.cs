using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.City;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.City
{
    public interface ICityAdminService
    {
        Task AddImagesAsync(Guid cityId, IEnumerable<Image> images);
        Task<IEnumerable<CityForAdminDTO>> GetByPageAsync(PaginationDTO pagination);
        Task<int> GetSearchCountAsync(string searchQuery);
        Task<IEnumerable<CityForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);
    }
}