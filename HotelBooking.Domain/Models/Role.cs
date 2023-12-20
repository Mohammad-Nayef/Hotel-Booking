using HotelBooking.Domain.Abstractions;

namespace HotelBooking.Domain.Models
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
