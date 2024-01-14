using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    public interface IHotelVisitRepository
    {
        Task AddAsync(HotelVisitDTO newVisit);
    }
}
