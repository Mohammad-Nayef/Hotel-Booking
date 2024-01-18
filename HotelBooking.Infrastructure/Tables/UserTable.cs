namespace HotelBooking.Infrastructure.Tables
{
    internal class UserTable : DbEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<RoleTable> Roles { get; } = new();
        public List<HotelReviewTable> Reviews { get; } = new();
        public List<HotelVisitTable> Visits { get; } = new();
        public List<CartItemTable> CartItems { get; } = new();
        public List<BookingTable> Bookings { get; } = new();
    }
}
