namespace HotelBooking.Infrastructure.Tables
{
    internal class HotelReviewTable : DbEntity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid HotelId { get; set; }
        public HotelTable Hotel { get; set; }
        public Guid UserId { get; set; }
        public UserTable User { get; set; }
    }
}
