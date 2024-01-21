namespace HotelBooking.Domain.Models.City
{
    public class PopularCityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public Guid? ThumbnailId { get; set; }
    }
}
