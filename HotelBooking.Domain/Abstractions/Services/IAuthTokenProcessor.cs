using HotelBooking.Domain.Models.User;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IAuthTokenProcessor
    {
        string GenerateToken(UserDTO user);
    }
}
