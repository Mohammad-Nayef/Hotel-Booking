using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }
        public Location? Location { get; set; }
    }
}
