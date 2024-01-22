using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="HotelVisitDTO"/>
    internal class HotelVisitTable : DbEntity
    {
        /// <inheritdoc cref="HotelVisitDTO.Date"/>
        public DateTime Date { get; set; }

        /// <inheritdoc cref="HotelVisitDTO.HotelId"/>
        public Guid HotelId { get; set; }

        public HotelTable Hotel { get; set; }

        /// <inheritdoc cref="HotelVisitDTO.UserId"/>
        public Guid UserId { get; set; }

        public UserTable User { get; set; }
    }
}
