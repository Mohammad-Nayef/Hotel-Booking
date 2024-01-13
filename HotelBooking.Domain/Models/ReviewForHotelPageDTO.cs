namespace HotelBooking.Domain.Models
{
    public class ReviewForHotelPageDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public UserForHotelReviewDTO User { get; set; }
    }
}
