using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;

namespace HotelBooking.Domain.Abstractions.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Stores a new user if its Id and username are not already stored.
        /// </summary>
        /// <param name="user">The user to be stored.</param>
        /// <exception cref="IdDuplicationException"></exception>
        /// <exception cref="UsernameDuplicationException"></exception>
        Task AddAsync(UserDTO user);
    }
}
