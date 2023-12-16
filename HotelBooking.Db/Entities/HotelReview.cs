using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class HotelReview
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        [Length(1, 200)]
        public string Content { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public Guid HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
