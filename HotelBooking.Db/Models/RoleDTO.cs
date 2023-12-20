using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class RoleDTO : Entity
    {
        internal string Name { get; set; }
        internal List<UserDTO> Users { get; } = new();
    }
}
