using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models.City
{
    public class CityDTO : Entity
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
