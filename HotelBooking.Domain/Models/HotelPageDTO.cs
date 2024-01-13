namespace HotelBooking.Domain.Models
{
    public class HotelPageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string Geolocation { get; set; }
        public CityForHotelPageDTO City { get; set; }
        public List<Guid> ImagesIds { get; set; }
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
