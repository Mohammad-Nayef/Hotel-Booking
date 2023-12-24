using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface ITokenGenerator
    {
        public string GenerateToken(UserDTO user);
    }
}
