using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<Guid> AddAsync(UserDTO user);
    }
}
