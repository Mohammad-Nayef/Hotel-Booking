using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Abstractions.Services
{
    /// <summary>
    /// Responsible for processing main operations for user.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add new user to the system.
        /// </summary>
        /// <returns>Id of the added user.</returns>
        Task<Guid> AddAsync(UserDTO user);

        /// <summary>
        /// Make sure the user credentials are valid.
        /// </summary>
        /// <returns>Authentication token if the user is authenticated.</returns>
        /// <exception cref="InvalidUserCredentialsException"></exception>
        Task<string> AuthenticateAsync(UserLoginDTO userLogin);

        /// <summary>
        /// Determine whether the user exists in the system or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid userId);
    }
}
