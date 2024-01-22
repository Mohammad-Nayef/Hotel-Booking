using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="RoleDTO"/>
    internal class RoleTable : DbEntity
    {
        /// <inheritdoc cref="RoleDTO.Name"/>
        public string Name { get; set; }

        public List<UserTable> Users { get; } = new();
    }
}
