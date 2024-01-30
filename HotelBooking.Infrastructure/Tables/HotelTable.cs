using HotelBooking.Domain.Models.Hotel;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="HotelDTO"/>
    internal class HotelTable : DbEntity
    {
        /// <inheritdoc cref="HotelDTO.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="HotelDTO.BriefDescription"/>
        public string BriefDescription { get; set; }

        /// <inheritdoc cref="HotelDTO.FullDescription"/>
        public string FullDescription { get; set; }

        /// <inheritdoc cref="HotelDTO.StarRating"/>
        public float StarRating { get; set; }

        /// <inheritdoc cref="HotelDTO.OwnerName"/>
        public string OwnerName { get; set; }

        /// <inheritdoc cref="HotelDTO.Geolocation"/>
        public string Geolocation { get; set; }

        /// <inheritdoc cref="HotelDTO.CreationDate"/>
        public DateTime CreationDate { get; set; }

        /// <inheritdoc cref="HotelDTO.ModificationDate"/>
        public DateTime ModificationDate { get; set; }

        /// <inheritdoc cref="HotelDTO.CityId"/>
        public Guid CityId { get; set; }

        public CityTable City { get; set; }

        public List<HotelReviewTable> Reviews { get; } = new();

        public List<RoomTable> Rooms { get; } = new();

        public List<DiscountTable> Discounts { get; } = new();

        public List<HotelVisitTable> Visits { get; } = new();
    }
}
