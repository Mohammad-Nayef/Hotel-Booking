using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Entities
{
    public class City : Entity
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Image> Images { get; set; }
    }
}
