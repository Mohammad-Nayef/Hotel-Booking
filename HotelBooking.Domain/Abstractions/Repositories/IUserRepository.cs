using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing main operations for user storage.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Store a new user.
        /// </summary>
        /// <returns>Id of the created user.</returns>
        Task<Guid> AddAsync(UserDTO newUser);

        /// <summary>
        /// Determine whether a user exists or not.
        /// </summary>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Get a user by their Id.
        /// </summary>
        Task<UserDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Get user with its roles by its username 
        /// </summary>
        Task<UserDTO> GetByUsernameIncludingRolesAsync(string username);

        /// <summary>
        /// Determine whether a username of a user exists or not.
        /// </summary>
        Task<bool> UsernameExistsAsync(string username);
    }
}
