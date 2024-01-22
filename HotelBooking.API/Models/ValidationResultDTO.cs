namespace HotelBooking.Api.Models
{
    /// <summary>
    /// Hold properties to show for the client.
    /// </summary>
    public class ValidationResultDTO
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
