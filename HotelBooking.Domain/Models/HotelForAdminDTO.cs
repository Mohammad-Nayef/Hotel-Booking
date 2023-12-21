namespace HotelBooking.Domain.Models
{
    public class HotelForAdminDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float StarRating { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
