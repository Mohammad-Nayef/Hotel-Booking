using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleDTO> GetByNameAsync(string roleName);
    }
}
