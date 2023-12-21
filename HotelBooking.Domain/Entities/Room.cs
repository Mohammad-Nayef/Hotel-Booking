using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    public class Room : Entity
    {
        public double Number { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Hotel Hotel { get; set; }
        public List<Image> Images { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
