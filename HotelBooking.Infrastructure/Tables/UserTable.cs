using HotelBooking.Domain.Models.User;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="UserDTO"/>
    internal class UserTable : DbEntity
    {
        /// <inheritdoc cref="UserDTO.FirstName"/>
        public string FirstName { get; set; }

        /// <inheritdoc cref="UserDTO.LastName"/>
        public string LastName { get; set; }

        /// <inheritdoc cref="UserDTO.Email"/>
        public string Email { get; set; }

        /// <inheritdoc cref="UserDTO.Username"/>
        public string Username { get; set; }

        /// <inheritdoc cref="UserDTO.Password"/>
        public string Password { get; set; }

        public List<RoleTable> Roles { get; } = new();

        public List<HotelReviewTable> Reviews { get; } = new();

        public List<HotelVisitTable> Visits { get; } = new();

        public List<CartItemTable> CartItems { get; } = new();

        public List<BookingTable> Bookings { get; } = new();
    }
}
