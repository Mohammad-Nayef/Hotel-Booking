namespace HotelBooking.Api.Models
{
    public class ValidationResultDTO
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
        public string AttemptedValue { get; set; }
    }
}
