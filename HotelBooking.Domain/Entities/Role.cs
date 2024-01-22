using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    /// <summary>
    /// Group of permissions for users.
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Name of the role.
        /// </summary>
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
