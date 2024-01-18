namespace HotelBooking.Infrastructure.Tables
{
    internal class RoleTable : DbEntity
    {
        public string Name { get; set; }
        public List<UserTable> Users { get; } = new();
    }
}
