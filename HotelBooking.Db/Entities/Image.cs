using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Image
    {
        public Guid Id { get; } = Guid.NewGuid();
        [Required]
        public byte[] FullImage { get; set; }
        public byte[] Thumbnail { get; set; }
        public Guid? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }
        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
