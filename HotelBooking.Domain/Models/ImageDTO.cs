using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class ImageDTO : Entity
    {
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public Guid? HotelId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? CityId { get; set; }
    }
}
