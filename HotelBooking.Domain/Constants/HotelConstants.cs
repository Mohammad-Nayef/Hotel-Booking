namespace HotelBooking.Domain.Constants
{
    public class HotelConstants
    {
        public const int MinNameLength = 1;
        public const int MaxNameLength = 100;
        public const int MinBriefDescriptionLength = 1;
        public const int MaxBriefDescriptionLength = 150;
        public const int MinFullDescriptionLength = 1;
        public const int MaxFullDescriptionLength = 2000;
        public const float MinStarRating = 1;
        public const float MaxStarRating = 5;
        public const string GeolocationRegex =
            @"^((\-?|\+?)?\d+(\.\d+)?),\s*((\-?|\+?)?\d+(\.\d+)?)$";
    }
}
