using HotelBooking.Domain.Models.Image;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="ImageDTO"/>
    internal class ImageTable : DbEntity
    {
        /// <inheritdoc cref="ImageDTO.Path"/>
        public string Path { get; set; }

        /// <inheritdoc cref="ImageDTO.EntityId"/>
        public Guid EntityId { get; set; }
    }
}
