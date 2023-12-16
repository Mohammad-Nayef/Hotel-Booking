using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Discount
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Length(3, 100)]
        public string? Reason { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        public DateTime? EndingDate { get; set; }

        [Required]
        [Range(1, 100)]
        public float AmountPercent { get; set; }

        public Guid? HotelId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}
