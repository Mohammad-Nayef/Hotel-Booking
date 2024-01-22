using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing a token for a user.
    /// </summary>
    public interface IAuthTokenProcessor
    {
        /// <summary>
        /// Generate a token for a user.
        /// </summary>
        string GenerateToken(UserDTO user);
    }
}
