using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    /// <inheritdoc cref="Entities.Image"/>
    public class ImageDTO : Entity
    {
        /// <inheritdoc cref="Entities.Image.Path"/>
        public string Path { get; set; }

        /// <inheritdoc cref="Entities.Image.ThumbnailPath"/>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Id of the hotel if the image is for an hotel.
        /// </summary>
        public Guid? HotelId { get; set; }

        /// <summary>
        /// Id of the room if the image is for a room.
        /// </summary>
        public Guid? RoomId { get; set; }

        /// <summary>
        /// Id of the city if the image is for a city.
        /// </summary>
        public Guid? CityId { get; set; }
    }
}
