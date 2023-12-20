using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class HotelReviewDTO : Entity
    {
        internal string Content { get; set; }
        internal DateTime CreationDate { get; set; }
        internal Guid HotelId { get; set; }
        internal HotelDTO Hotel { get; set; }
        internal Guid UserId { get; set; }
        internal UserDTO User { get; set; }
    }
}
