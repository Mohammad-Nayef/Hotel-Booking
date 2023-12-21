using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Models
{
    public class PaginationMetadataDTO
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PaginationMetadataDTO(int totalItems, PaginationDTO pagination)
        {
            TotalItems = totalItems;
            PageSize = pagination.PageSize;
            CurrentPage = pagination.PageNumber;
        }
    }
}
