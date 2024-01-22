using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.User
{
    /// <summary>
    /// Model for user who created an hotel review.
    /// </summary>
    public class UserForHotelReviewDTO : Entity
    {
        /// <inheritdoc cref="UserDTO.FirstName"/>
        public string FirstName { get; set; }

        /// <inheritdoc cref="UserDTO.LastName"/>
        public string LastName { get; set; }
    }
}
