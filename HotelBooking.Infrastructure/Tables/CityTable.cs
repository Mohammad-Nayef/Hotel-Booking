namespace HotelBooking.Infrastructure.Tables
{
    internal class CityTable : DbEntity
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<HotelTable> Hotels { get; } = new();
        public List<ImageTable> Images { get; } = new();
    }
}
