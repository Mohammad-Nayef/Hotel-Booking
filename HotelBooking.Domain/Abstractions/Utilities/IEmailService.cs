using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Utilities
{
    public interface IEmailService
    {
        Task SendAsync(EmailDTO email);
    }
}
