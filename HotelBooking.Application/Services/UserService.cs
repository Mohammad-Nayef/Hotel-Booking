using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Models;
using Microsoft.AspNet.Identity;

namespace HotelBooking.Application.Services
{
    internal class UserService : EntityService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task AddAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            _userRepository.ThrowExceptionIfIdOrUsernameExists(user);

            user.Password = _passwordHasher.HashPassword(user.Password);

            await _userRepository.AddAsync(user);
        }
    }
}
