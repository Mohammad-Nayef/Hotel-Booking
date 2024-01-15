namespace HotelBooking.Domain.Constants
{
    public class UserConstants
    {
        public const string NameRegex = @"^[A-Za-z\s]+$";
        public const string UsernameRegex = @"^[\w]+$";
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 50;
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 50;
    }
}
