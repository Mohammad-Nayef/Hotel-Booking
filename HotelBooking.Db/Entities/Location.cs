using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace HotelBooking.Db.Entities
{
    public class Location
    {
        public Guid Id { get; } = Guid.NewGuid();
        [Required]
        [Length(3, 100)]
        public string CityName { get; set; }
        [Required]
        [Length(3, 100)]
        public string CountryName { get; set; }
        [Required]
        [Length(3, 100)]
        public string StreetName { get; set; }
        [Required]
        [Length(3, 100)]
        public string LocationOnMap { get; set; }
        [Required]
        [Length(3, 100)]
        public string PostOffice { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<Image> Images { get; } = new List<Image>();
    }
}
