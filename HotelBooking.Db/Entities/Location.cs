namespace HotelBooking.Db.Entities
{
    internal class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StreetName { get; set; }
        public string LocationOnMap { get; set; }
        public string PostOffice { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<Image> CityImages { get; } = new();
    }
}
