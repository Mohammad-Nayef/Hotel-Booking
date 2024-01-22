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

        /// <inheritdoc cref="ImageDTO.HotelId"/>
        public Guid? HotelId { get; set; }

        public HotelTable? Hotel { get; set; }

        /// <inheritdoc cref="ImageDTO.RoomId"/>
        public Guid? RoomId { get; set; }

        public RoomTable? Room { get; set; }

        /// <inheritdoc cref="ImageDTO.CityId"/>
        public Guid? CityId { get; set; }

        public CityTable? City { get; set; }
    }
}
