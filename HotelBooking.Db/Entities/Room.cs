using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Room
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        [Range(0d, double.MaxValue)]
        public double RoomNumber { get; set; }

        [Required]
        [Length(1, 100)]
        public string Type { get; set; }

        [Required]
        [Range(0, 20)]
        public int AdultsCapacity { get; set; }

        [Required]
        [Range(0, 20)]
        public int ChildrenCapacity { get; set; }

        [Required]
        [Length(3, 150)]
        public string BriefDescription { get; set; }

        [Required]
        [Range(0d, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        public Guid HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public List<Image> Images { get; } = new();

        public List<CartItem> CartItems { get; } = new();

        public List<Booking> Bookings { get; } = new();
    }
}
