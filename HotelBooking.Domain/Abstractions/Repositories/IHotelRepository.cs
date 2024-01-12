using System.Linq.Expressions;
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
        Task<HotelDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(HotelDTO hotel);
        IEnumerable<HotelForAdminDTO> SearchByHotelForAdminByPage(
            int itemsToSkip,
            int itemsToTake,
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchByHotelForAdminCountAsync(
            Expression<Func<HotelForAdminDTO, bool>> searchExpression);
        Task<int> GetNumberOfImagesAsync(Guid hotelId);
        IEnumerable<FeaturedHotelDTO> GetHotelsWithActiveDiscountsByPage(
            int itemsToSkip, int itemsToTake);
        IEnumerable<HotelForUserDTO> SearchForUserByPage(
            HotelSearchDTO hotelSearch, int itemsToSkip, int itemsToTake);

        int GetSearchForUserCount(HotelSearchDTO hotelSearch);
        int GetHotelsWithActiveDiscountsCount();
    }
}
