using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Exceptions
{
    public class EntityImagesLimitExceededException : Exception
    {
        public int ExceededLimit => ImagesConstants.MaxNumberOfImagesPerEntity;
    }
}
