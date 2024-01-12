using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Models
{
    public class HotelSearchDTO
    {
        public string SearchQuery { get; set; }
        public DateTime CheckinDate { get; set; } = HotelSearchConstants.DefaultCheckinDate;
        public DateTime CheckoutDate { get; set; } = HotelSearchConstants.DefaultCheckoutDate;
        public int NumberOfAdults { get; set; } = HotelSearchConstants.DefaultNumberOfAdults;
        public int NumberOfChildren { get; set; } = HotelSearchConstants.DefaultNumberOfChildren;
        public int NumberOfRooms { get; set; } = HotelSearchConstants.DefaultNumberOfRooms;
        public decimal MinRoomPrice { get; set; } = HotelSearchConstants.MinRoomPrice;
        public decimal MaxRoomPrice { get; set; } = HotelSearchConstants.MaxRoomPrice;
        public string RoomsType { get; set; } = string.Empty;
    }
}
