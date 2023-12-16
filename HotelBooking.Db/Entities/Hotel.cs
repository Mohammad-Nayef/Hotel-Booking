using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Hotel
    {
        public Guid Id { get; } = Guid.NewGuid();
        [Required]
        [Length(1, 100)]
        public string Name { get; set; }
        [Required]
        [Length(3, 150)]
        public string BriefDescription { get; set; }
        [Required]
        [Length(3, 2000)]
        public string FullDescription { get; set; }
        [Required]
        [Range(1, 5)]
        public float StarRating { get; set; }
        [Required]
        [Length(1, 50)]
        public string OwnerName { get; set; }
        public List<Review> Reviews { get; } = new List<Review>();
        public Location? Location { get; set; }
        public List<Image> Images { get; } = new List<Image>();
        public List<Room> Rooms { get; } = new List<Room>();
        public List<Discount> Discounts { get; } = new List<Discount>();
        public List<Visit> Visits { get; } = new List<Visit>();
    }
}
