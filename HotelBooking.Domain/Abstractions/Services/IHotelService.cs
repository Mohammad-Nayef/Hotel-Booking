using HotelBooking.Domain.Models;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IHotelService
    {
        Task<Guid> AddAsync(HotelDTO hotel);
        Task<bool> ExistsAsync(Guid Id);
        Task<int> GetCountAsync();
        Task<IEnumerable<HotelForAdminDTO>> GetForAdminByPageAsync(PaginationDTO pagination);
        Task DeleteAsync(Guid id);
        Task<HotelDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(HotelDTO hotel);
        Task<IEnumerable<HotelForAdminDTO>> SearchByHotelForAdminByPageAsync(
            PaginationDTO pagination, string searchQuery);
        Task<int> GetSearchByHotelForAdminCountAsync(string searchQuery);
        Task AddImagesForHotelAsync(Guid hotelId, IEnumerable<Image> images);
        Task<IEnumerable<FeaturedHotelDTO>> GetFeaturedHotelsByPageAsync(PaginationDTO pagination);
        Task<IEnumerable<HotelForUserDTO>> SearchForUserByPageAsync(
            HotelSearchDTO hotelSearch, PaginationDTO pagination);
        int GetSearchForUserCount(HotelSearchDTO hotelSearch);
        int GetFeaturedHotelsCount();
        Task<HotelPageDTO> GetHotelPageAsync(Guid hotelId);
        Task<IEnumerable<ReviewForHotelPageDTO>> GetReviewsByPageAsync(
            Guid id, PaginationDTO pagination);
        Task<int> GetReviewsCountAsync(Guid id);
    }
}
