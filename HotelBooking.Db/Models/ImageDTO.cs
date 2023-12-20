using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class ImageDTO : Entity
    {
        internal string Path { get; set; }
        internal string ThumbnailPath { get; set; }
        internal Guid? HotelId { get; set; }
        internal HotelDTO? Hotel { get; set; }
        internal Guid? RoomId { get; set; }
        internal RoomDTO? Room { get; set; }
        internal Guid? LocationId { get; set; }
        internal LocationDTO? Location { get; set; }
    }
}
