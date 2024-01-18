namespace HotelBooking.Infrastructure.Tables
{
    internal class ImageTable : DbEntity
    {
        public string Path { get; set; }
        public string? ThumbnailPath { get; set; }
        public Guid? HotelId { get; set; }
        public HotelTable? Hotel { get; set; }
        public Guid? RoomId { get; set; }
        public RoomTable? Room { get; set; }
        public Guid? CityId { get; set; }
        public CityTable? City { get; set; }
    }
}
