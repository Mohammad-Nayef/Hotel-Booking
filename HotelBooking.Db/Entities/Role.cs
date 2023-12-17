namespace HotelBooking.Db.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<User> Users { get; } = new();
    }
}
