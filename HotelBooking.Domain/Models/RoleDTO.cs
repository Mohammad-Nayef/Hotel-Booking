using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.Role"/>
    public class RoleDTO : Entity
    {
        /// <inheritdoc cref="Entities.Role.Name"/>
        public string Name { get; set; }
    }
}
