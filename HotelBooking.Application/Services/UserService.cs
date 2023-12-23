using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNet.Identity;

namespace HotelBooking.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<UserDTO> _userValidator;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IValidator<UserDTO> userValidator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _userValidator = userValidator;
        }

        public async Task<Guid> AddAsync(UserDTO newUser)
        {
            await _userValidator.ValidateAndThrowAsync(newUser);

            newUser.Password = _passwordHasher.HashPassword(newUser.Password);

            return await _userRepository.AddAsync(newUser);
        }
    }
}
