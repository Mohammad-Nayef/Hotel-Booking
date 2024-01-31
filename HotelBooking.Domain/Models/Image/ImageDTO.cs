using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.Image
{
    /// <inheritdoc cref="Entities.Image"/>
    public class ImageDTO : Entity
    {
        /// <inheritdoc cref="Entities.Image.Path"/>
        public string Path { get; set; }

        /// <inheritdoc cref="Entities.Image.EntityId"/>
        public Guid EntityId { get; set; }
    }
}
