using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Models.Hotel
{
    /// <summary>
    /// Contains properties for querying and filtering hotels for user search.
    /// </summary>
    public class HotelSearchDTO
    {
        /// <summary>
        /// Textual search query to find hotels.
        /// </summary>
        /// <remarks>
        /// Must be of length between <see cref="HotelSearchConstants.MinSearchQueryLength"/> and 
        /// <see cref="HotelSearchConstants.MaxSearchQueryLength"/>.
        /// </remarks>
        public string SearchQuery { get; set; }
        /// <summary>
        /// Time for starting the booking interval.
        /// </summary>
        /// <remarks>
        /// Can't be in the past. Default is 
        /// <see cref="HotelSearchConstants.DefaultCheckinDate"/>.
        /// </remarks>
        public DateTime CheckinDate { get; set; } = HotelSearchConstants.DefaultCheckinDate;
        /// <summary>
        /// Time for ending the booking interval.
        /// </summary>
        /// <remarks>
        /// Can't be in the past and must be after <see cref="CheckinDate"/>. Default is
        /// <see cref="HotelSearchConstants.DefaultCheckoutDate"/>.
        /// </remarks>
        public DateTime CheckoutDate { get; set; } = HotelSearchConstants.DefaultCheckoutDate;
        /// <summary>
        /// Number of adults for the booking.
        /// </summary>
        /// <remarks>
        /// Must be between <see cref="RoomConstants.MinAdultsCapacity"/> and 
        /// <see cref="RoomConstants.MaxAdultsCapacity"/>. Default is 
        /// <see cref="HotelSearchConstants.DefaultNumberOfAdults"/>.
        /// </remarks>
        public int NumberOfAdults { get; set; } = HotelSearchConstants.DefaultNumberOfAdults;
        /// <summary>
        /// Number of children for  the booking.
        /// </summary>
        /// <remarks>
        /// Must be between <see cref="RoomConstants.MinChildrenCapacity"/> and 
        /// <see cref="RoomConstants.MaxChildrenCapacity"/>. Default is 
        /// <see cref="HotelSearchConstants.DefaultNumberOfChildren"/>.
        /// </remarks>
        public int NumberOfChildren { get; set; } = HotelSearchConstants.DefaultNumberOfChildren;
        /// <summary>
        /// Number of needed rooms for the booking.
        /// </summary>
        /// <remarks>Default is <see cref="HotelSearchConstants.DefaultNumberOfRooms"/>.</remarks>
        public int NumberOfRooms { get; set; } = HotelSearchConstants.DefaultNumberOfRooms;
        /// <summary>
        /// Minimum price of a room for a single day.
        /// </summary>
        /// <remarks>Default is <see cref="HotelSearchConstants.MinRoomPrice"/>.</remarks>
        public decimal MinRoomPrice { get; set; } = HotelSearchConstants.MinRoomPrice;
        /// <summary>
        /// Maximum price of a room for a single day.
        /// </summary>
        /// <remarks>
        /// Can't be less that <see cref="MinRoomPrice"/>. Default is 
        /// <see cref="HotelSearchConstants.MaxRoomPrice"/>.
        /// </remarks>
        public decimal MaxRoomPrice { get; set; } = HotelSearchConstants.MaxRoomPrice;
        /// <summary>
        /// Type of rooms to look for.
        /// </summary>
        /// <remarks>
        /// Can't be of length more than <see cref="RoomConstants.MaxTypeLength"/>
        /// </remarks>
        public string RoomsType { get; set; } = string.Empty;
    }
}
