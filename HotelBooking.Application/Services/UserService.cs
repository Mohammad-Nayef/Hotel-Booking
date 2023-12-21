using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Models;
using Microsoft.AspNet.Identity;

namespace HotelBooking.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IValidator<PaginationDTO> paginationValidator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task AddAsync(UserDTO user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            _userRepository.ThrowExceptionIfIdOrUsernameExists(user);

            user.Password = _passwordHasher.HashPassword(user.Password);

            //await _userRepository.AddAsync(user);
        }
    }
}
