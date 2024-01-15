namespace HotelBooking.Domain.Models.City
{
    public class CityForUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
    }
}
