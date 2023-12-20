using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Location : Entity
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StreetName { get; set; }
        public string LocationOnMap { get; set; }
        public string PostOffice { get; set; }
        public Hotel Hotel { get; set; }
        public List<Image> CityImages { get; set; }
    }
}
