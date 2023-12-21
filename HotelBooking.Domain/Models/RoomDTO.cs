using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class RoomDTO : Entity
    {
        public double Number { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid HotelId { get; set; }
    }
}
