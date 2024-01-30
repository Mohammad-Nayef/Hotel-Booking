using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Model to view visited hotel for user.
    /// </summary>
    public class VisitedHotelDTO : Entity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float StarRating { get; set; }

        public CityForUserDTO City { get; set; }
    }
}
