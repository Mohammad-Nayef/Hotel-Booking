using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Exceptions
{
    /// <summary>
    /// The limit for number of images per entity is exceeded.
    /// </summary>
    public class EntityImagesLimitExceededException : Exception
    {
        /// <summary>
        /// Limit of number of images.
        /// </summary>
        public int ExceededLimit => ImagesConstants.MaxNumberOfImagesPerEntity;
    }
}
