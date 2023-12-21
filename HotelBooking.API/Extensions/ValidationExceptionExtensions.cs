using FluentValidation;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Extensions
{
    internal static class ValidationExceptionExtensions
    {
        public static IEnumerable<ValidationResultDTO> GetErrorsForClient(
            this ValidationException validationException)
        {
            return validationException.Errors.Select(error => new ValidationResultDTO
            {
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName,
                AttemptedValue = error.AttemptedValue.ToString()
            });
        }
    }
}
