namespace HotelBooking.Domain.Models
{
    public class CityForAdminDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
        public int NumberOfHotels { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
