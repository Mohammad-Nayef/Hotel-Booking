using HotelBooking.Domain.Models.City;

namespace HotelBooking.Domain.Models.Hotel
{
    public class VisitedHotelDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float StarRating { get; set; }
        public CityForUserDTO City { get; set; }
        public Guid ThumbnailId { get; set; }
    }
}
