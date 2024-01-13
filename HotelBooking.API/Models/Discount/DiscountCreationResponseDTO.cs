namespace HotelBooking.Api.Models.Discount
{
    public class DiscountCreationResponseDTO
    {
        public Guid Id { get; set; }
        public string? Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public float AmountPercent { get; set; }
        public Guid HotelId { get; set; }
    }
}
