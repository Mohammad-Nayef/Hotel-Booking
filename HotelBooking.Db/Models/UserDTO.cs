using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class UserDTO : Entity
    {
        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal string Email { get; set; }
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal List<RoleDTO> Roles { get; } = new();
        internal List<HotelReviewDTO> Reviews { get; } = new();
        internal List<VisitDTO> Visits { get; } = new();
        internal List<CartItemDTO> CartItems { get; } = new();
        internal List<BookingDTO> Bookings { get; } = new();
    }
}
