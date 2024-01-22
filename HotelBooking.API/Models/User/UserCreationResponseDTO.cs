using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Api.Models.User
{
    public class UserCreationResponseDTO : Entity
    {
        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; }
    }
}
