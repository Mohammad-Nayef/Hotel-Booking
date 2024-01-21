namespace HotelBooking.Domain.Constants
{
    public class HotelVisitConstants
    {
        public readonly static DateTime LeastRecentVisitDate = DateTime.UtcNow.AddDays(-15);
    }
}
