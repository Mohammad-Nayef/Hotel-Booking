using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories.Hotel
{
    /// <summary>
    /// Responsible for managing hotel visits.
    /// </summary>
    public interface IHotelVisitRepository
    {
        /// <summary>
        /// Store new visit for an hotel.
        /// </summary>
        Task AddAsync(HotelVisitDTO newVisit);
    }
}
