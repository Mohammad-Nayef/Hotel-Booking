using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class UserDTO : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<RoleDTO> Roles { get; } = new();
    }
}
