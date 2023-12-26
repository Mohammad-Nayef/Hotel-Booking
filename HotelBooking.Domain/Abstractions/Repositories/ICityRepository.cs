using System.Linq.Expressions;
using HotelBooking.Domain.Models;
using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface ICityRepository
    {
        Task<Guid> AddAsync(CityDTO newCity);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        IEnumerable<CityForAdminDTO> GetForAdminByPage(int itemsToSkip, int itemsToTake);
        Task<CityDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CityDTO city);
        IEnumerable<CityForAdminDTO> SearchByCityForAdminByPage(
            int itemsToSkip, 
            int itemsToTake,
            Expression<Func<CityForAdminDTO, bool>> searchExpression);
        Task<int> GetSearchByCityForAdminCountAsync(
            Expression<Func<CityForAdminDTO, bool>> searchExpression);
        Task AddImagesAsync(Guid cityId, IEnumerable<Image> images);
        Task<int> GetNumberOfImagesAsync(Guid cityId);
    }
}
