using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(UserDTO newUser);
        Task<bool> ExistsAsync(Guid id);
        Task<UserDTO> GetByUsernameIncludingRolesAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
    }
}
