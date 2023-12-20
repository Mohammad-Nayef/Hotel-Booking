using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class HotelDTO : Entity
    {
        internal string Name { get; set; }
        internal string BriefDescription { get; set; }
        internal string FullDescription { get; set; }
        internal float StarRating { get; set; }
        internal string OwnerName { get; set; }
        internal List<HotelReviewDTO> Reviews { get; } = new();
        internal LocationDTO Location { get; set; }
        internal List<ImageDTO> Images { get; } = new();
        internal List<RoomDTO> Rooms { get; } = new();
        internal List<DiscountDTO> Discounts { get; } = new();
        internal List<VisitDTO> Visits { get; } = new();
    }
}
