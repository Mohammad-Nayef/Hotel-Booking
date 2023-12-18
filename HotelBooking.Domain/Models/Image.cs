using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public Guid? HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public Guid? RoomId { get; set; }
        public Room? Room { get; set; }
        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
