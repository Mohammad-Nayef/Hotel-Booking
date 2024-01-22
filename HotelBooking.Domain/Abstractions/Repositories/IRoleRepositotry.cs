using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Responsible for managing main operations for role storage.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Get a role by its name or create a new one if it doesn't exist.
        /// </summary>
        Task<RoleDTO> GetByNameAsync(string roleName);
    }
}
