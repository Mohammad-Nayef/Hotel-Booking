namespace HotelBooking.Domain.Configurations
{
    public class EmailConfigurations
    {
        public string SenderName { get; init; }
        public string SenderEmail { get; init; }
        public string SenderPassword { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
    }
}
