namespace HotelBooking.Db.Tables
{
    internal class ImageTable : DbEntity
    {
        public string Path { get; set; }
        public string? ThumbnailPath { get; set; }
        public Guid? HotelId { get; set; }
        public HotelTable? Hotel { get; set; }
        public Guid? RoomId { get; set; }
        public RoomTable? Room { get; set; }
        public Guid? LocationId { get; set; }
        public CityTable? City { get; set; }
    }
}
