using System.Text.Json;
using HotelBooking.Api.Models;
using HotelBooking.Domain.Models;

namespace HotelBooking.Api.Extensions
{
    internal static class ResponseHeadersExtensions
    {
        public const string PaginationKeyName = "X-Pagination";

        public static void AddPaginationMetadata(
            this IHeaderDictionary headers,
            int totalItems,
            PaginationDTO pagination)
        {
            var paginationMetadata = new PaginationMetadataDTO(totalItems, pagination);
            var jsonPaginationMetadata = JsonSerializer.Serialize(paginationMetadata);

            headers.Add(PaginationKeyName, jsonPaginationMetadata);
        }
    }
}
