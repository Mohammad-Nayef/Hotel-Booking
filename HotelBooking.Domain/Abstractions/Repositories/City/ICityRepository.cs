using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Abstractions.Repositories.City
{
    public interface ICityRepository
    {
        Task<Guid> AddAsync(CityDTO newCity);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync();
        Task DeleteAsync(Guid id);
        Task<CityDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CityDTO city);
        IEnumerable<PopularCityDTO> GetPopularCitiesByPage(
            int itemsToSkip, int itemsToTake);
    }
}
