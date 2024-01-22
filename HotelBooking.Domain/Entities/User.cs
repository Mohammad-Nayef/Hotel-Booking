using HotelBooking.Domain.Abstractions;
using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// User of the hotel booking system.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// First name of the user.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="UserConstants.MinNameLength"/> and 
        /// <see cref="UserConstants.MaxNameLength"/>. It must match 
        /// <see cref="UserConstants.NameRegex"/>.
        /// </remarks>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="UserConstants.MinNameLength"/> and 
        /// <see cref="UserConstants.MaxNameLength"/>. It must match 
        /// <see cref="UserConstants.NameRegex"/>.
        /// </remarks>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        /// <remarks>
        /// Must be unique and of length between
        /// <see cref="UserConstants.MinUsernameLength"/> and 
        /// <see cref="UserConstants.MaxUsernameLength"/>. It must match 
        /// <see cref="UserConstants.UsernameRegex"/>.
        /// </remarks>
        public string Username { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="UserConstants.MinPasswordLength"/> and 
        /// <see cref="UserConstants.MaxPasswordLength"/>.
        /// </remarks>
        public string Password { get; set; }

        public List<Role> Roles { get; set; }

        public List<HotelReview> Reviews { get; set; }

        public List<HotelVisit> Visits { get; set; }

        public List<CartItem> CartItems { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
