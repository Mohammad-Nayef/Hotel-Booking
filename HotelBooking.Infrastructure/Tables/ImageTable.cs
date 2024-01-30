using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="ImageDTO"/>
    internal class ImageTable : DbEntity
    {
        /// <inheritdoc cref="ImageDTO.Path"/>
        public string Path { get; set; }

        /// <inheritdoc cref="ImageDTO.ThumbnailPath"/>
        public string? ThumbnailPath { get; set; }

        public Guid EntityId { get; set; }
    }
}
