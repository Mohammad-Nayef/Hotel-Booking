namespace HotelBooking.Domain.Models.Room
{
    public class RoomForUserDTO
    {
        public Guid Id { get; set; }
        public double Number { get; set; }
        public string Type { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string BriefDescription { get; set; }
        public decimal PricePerNight { get; set; }
        public DiscountDTO CurrentDiscount { get; set; }
        public List<Guid> ImagesIds { get; set; }
    }
}
