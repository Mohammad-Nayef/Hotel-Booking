namespace HotelBooking.Domain.Configurations
{
    public class JwtConfigurations
    {
        public string Key { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public double ExpirationMinutes { get; init; }
    }
}
