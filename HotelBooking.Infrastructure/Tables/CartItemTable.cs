using HotelBooking.Domain.Models;

namespace HotelBooking.Infrastructure.Tables
{
    /// <inheritdoc cref="CartItemDTO"/>
    internal class CartItemTable : DbEntity
    {
        /// <inheritdoc cref="CartItemDTO.AddingDate"/>
        public DateTime AddingDate { get; set; }

        /// <inheritdoc cref="CartItemDTO.RoomId"/>
        public Guid RoomId { get; set; }

        public RoomTable Room { get; set; }

        /// <inheritdoc cref="CartItemDTO.UserId"/>
        public Guid UserId { get; set; }

        public UserTable User { get; set; }
    }
}
