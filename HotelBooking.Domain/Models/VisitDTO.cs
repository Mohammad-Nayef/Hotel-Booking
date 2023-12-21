using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class VisitDTO : Entity
    {
        public DateTime Date { get; set; }
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
    }
}
