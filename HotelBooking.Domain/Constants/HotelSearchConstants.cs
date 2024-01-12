namespace HotelBooking.Domain.Constants
{
    public class HotelSearchConstants
    {
        public const int DefaultNumberOfAdults = 2;
        public const int DefaultNumberOfChildren = 0;
        public static readonly DateTime DefaultCheckinDate = DateTime.UtcNow;
        public static readonly DateTime DefaultCheckoutDate = DateTime.UtcNow.AddDays(1);
        public const int DefaultNumberOfRooms = 1;
        public const int MinSearchQueryLength = 1;
        public const int MaxSearchQueryLength = 100;
        public const decimal MinRoomPrice = 0;
        public const decimal MaxRoomPrice = decimal.MaxValue;
    }
}
