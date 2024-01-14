using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class HotelVisitDTO : Entity
    {
        public DateTime Date { get; set; }
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
    }
}
