using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class VisitDTO : Entity
    {
        internal DateTime Date { get; set; }
        internal Guid HotelId { get; set; }
        internal HotelDTO Hotel { get; set; }
        internal Guid UserId { get; set; }
        internal UserDTO User { get; set; }
    }
}
