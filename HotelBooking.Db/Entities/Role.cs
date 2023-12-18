namespace HotelBooking.Db.Entities
{
    internal class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<User> Users { get; } = new();
    }
}
