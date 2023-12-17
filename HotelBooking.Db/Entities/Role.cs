using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Db.Entities
{
    public class Role
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        [Length(1, 50)]
        public string Name { get; set; }

        public List<User> Users { get; } = new();
    }
}
