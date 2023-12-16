using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Visit
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; }

        public Guid HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
