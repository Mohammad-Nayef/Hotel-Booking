using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Searches for the existence of the Id, if not found, it searches for the
        /// existence of the username.
        /// </summary>
        /// <param name="user">The user to search for its Id and username</param>
        /// <exception cref="IdDuplicationException"></exception>
        /// <exception cref="UsernameDuplicationException"></exception>
        void ThrowExceptionIfIdOrUsernameExists(UserDTO user);
    }
}
