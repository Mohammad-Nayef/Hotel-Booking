using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Db.Models
{
    internal class LocationDTO : Entity
    {
        internal string CityName { get; set; }
        internal string CountryName { get; set; }
        internal string StreetName { get; set; }
        internal string LocationOnMap { get; set; }
        internal string PostOffice { get; set; }
        internal Guid HotelId { get; set; }
        internal HotelDTO Hotel { get; set; }
        internal List<ImageDTO> CityImages { get; } = new();
    }
}
