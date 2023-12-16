using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class CartItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public DateTime AddingDate { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
