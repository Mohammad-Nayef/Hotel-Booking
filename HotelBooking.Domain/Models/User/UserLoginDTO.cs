namespace HotelBooking.Domain.Models.User
{
    /// <summary>
    /// Model for user to login.
    /// </summary>
    public class UserLoginDTO
    {
        /// <inheritdoc cref="UserDTO.Username"/>
        public string Username { get; set; }

        /// <inheritdoc cref="UserDTO.Password"/>
        public string Password { get; set; }
    }
}
