namespace HotelBooking.Domain.Models
{
    public class FeaturedHotelDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public float? StarRating { get; set; }
        public Guid? ThumbnailId { get; set; }
        public DiscountDTO CurrentDiscount { get; set; }
    }
}
