using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Models.Hotel
{
    public class HotelPageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public string FullDescription { get; set; }
        public float StarRating { get; set; }
        public string Geolocation { get; set; }
        public CityForUserDTO City { get; set; }
        public List<Guid> ImagesIds { get; set; }
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
