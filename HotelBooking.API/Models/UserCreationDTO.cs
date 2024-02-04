namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Model to create new user.
    /// </summary>
    public class UserCreationDTO
    {
        /// <summary>
        /// First name of the user.
        /// Must be of length between 3 and 50. It must match: ^[A-Za-z\s]+$
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// Must be of length between 3 and 50. It must match: ^[A-Za-z\s]+$
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username of the user.
        /// Must be unique and of length between 3 and 50. It must match: ^[\w]+$
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of the user.
        /// Must be of length between 8 and 50.
        /// </summary>
        public string Password { get; set; }
    }
}
