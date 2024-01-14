using HotelBooking.Domain.Models;
using HotelBooking.Domain.Models.Hotel;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services.Hotel
{
    public interface IHotelAdminService
    {
        Task<IEnumerable<HotelForAdminDTO>> GetByPageAsync(PaginationDTO pagination);
        Task<IEnumerable<HotelForAdminDTO>> SearchByPageAsync(
            PaginationDTO pagination, string searchQuery);
        Task<int> GetSearchCountAsync(string searchQuery);
        Task AddImagesAsync(Guid hotelId, IEnumerable<Image> images);
    }
}
