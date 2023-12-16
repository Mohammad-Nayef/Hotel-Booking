using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Booking
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
