using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public List<HotelReview> Reviews { get; set; }
        public List<Visit> Visits { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
