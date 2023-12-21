using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Models
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; } = PaginationConstants.DefaultPageNumber;
        public int PageSize { get; set; } = PaginationConstants.DefaultPageSize;
    }
}
