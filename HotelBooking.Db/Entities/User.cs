using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        [Length(3, 50)]
        public string FirstName { get; set; }

        [Required]
        [Length(3, 50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email{ get; set; }

        [Required]
        [Length(3, 50)]
        public string Username { get; set; }

        [Required]
        [Length(8, 50)]
        public string Password { get; set; }

        public List<Role> Roles { get; } = new();

        public List<HotelReview> Reviews { get; } = new();

        public List<Visit> Visits { get; } = new();

        public List<CartItem> CartItems { get; } = new();

        public List<Booking> Bookings { get; } = new();
    }
}
