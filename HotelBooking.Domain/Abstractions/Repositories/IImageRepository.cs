using SixLabors.ImageSharp;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task AddForCityAsync(Guid cityId, IEnumerable<Image> images);
    }
}
