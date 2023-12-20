using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class DiscountDTO : Entity
    {
        internal string? Reason { get; set; }
        internal DateTime StartingDate { get; set; }
        internal DateTime? EndingDate { get; set; }
        internal float AmountPercent { get; set; }
        internal Guid HotelId { get; set; }
        internal HotelDTO Hotel { get; set; }
    }
}
