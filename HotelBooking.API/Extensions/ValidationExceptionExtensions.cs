using FluentValidation;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Extensions
{
    internal static class ValidationExceptionExtensions
    {
        /// <summary>
        /// Get only the necessary errors for the client.
        /// </summary>
        public static IEnumerable<ValidationResultDTO> GetErrorsForClient(
            this ValidationException validationException)
        {
            return validationException.Errors.Select(error => new ValidationResultDTO
            {
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName
            });
        }
    }
}
