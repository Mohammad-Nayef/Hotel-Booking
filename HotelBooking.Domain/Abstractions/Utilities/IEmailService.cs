using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Utilities
{
    /// <summary>
    /// Responsible for managing emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send an email from the email provided in the configurations.
        /// </summary>
        /// <param name="email">Details about the email to send.</param>
        Task SendAsync(EmailDTO email);
    }
}
