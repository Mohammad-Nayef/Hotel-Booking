using HotelBooking.Domain.Constants;

namespace HotelBooking.Domain.Models
{
    /// <summary>
    /// Details for applying pagination.
    /// </summary>
    public class PaginationDTO
    {
        /// <summary>
        /// Number of the needed page.
        /// </summary>
        /// <remarks>Default is <see cref="PaginationConstants.DefaultPageNumber"/>.</remarks>
        public int PageNumber { get; set; } = PaginationConstants.DefaultPageNumber;
        /// <summary>
        /// Size of the needed page.
        /// </summary>
        /// <remarks>Default is <see cref="PaginationConstants.DefaultPageSize"/>.</remarks>
        public int PageSize { get; set; } = PaginationConstants.DefaultPageSize;
    }
}
