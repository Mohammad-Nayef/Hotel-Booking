namespace HotelBooking.Db.Entities
{
    public class Discount
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public float AmountPercent { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
