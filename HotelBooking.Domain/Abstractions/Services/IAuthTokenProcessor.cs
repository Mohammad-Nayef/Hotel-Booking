using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IAuthTokenProcessor
    {
        string GenerateToken(UserDTO user);
    }
}
