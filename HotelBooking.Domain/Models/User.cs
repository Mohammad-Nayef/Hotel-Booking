using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; } = new();
        public List<HotelReview> Reviews { get; } = new();
        public List<Visit> Visits { get; } = new();
        public List<CartItem> CartItems { get; } = new();
        public List<Booking> Bookings { get; } = new();
    }
}
