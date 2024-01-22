using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.User
{
    /// <inheritdoc cref="Entities.User"/>
    public class UserDTO : Entity
    {
        /// <inheritdoc cref="Entities.User.FirstName"/>
        public string FirstName { get; set; }

        /// <inheritdoc cref="Entities.User.LastName"/>
        public string LastName { get; set; }

        /// <inheritdoc cref="Entities.User.Email"/>
        public string Email { get; set; }

        /// <inheritdoc cref="Entities.User.Username"/>
        public string Username { get; set; }

        /// <inheritdoc cref="Entities.User.Password"/>
        public string Password { get; set; }

        public List<RoleDTO> Roles { get; } = new();
    }
}
