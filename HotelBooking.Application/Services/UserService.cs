using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Abstractions.Services;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Exceptions;
using HotelBooking.Domain.Models;
using Microsoft.AspNet.Identity;

namespace HotelBooking.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IValidator<UserDTO> _userValidator;
        private readonly IValidator<UserLoginDTO> _userLoginValidator;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IValidator<UserDTO> userValidator,
            IValidator<UserLoginDTO> userLoginValidator,
            ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _userValidator = userValidator;
            _userLoginValidator = userLoginValidator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Guid> AddAsync(UserDTO newUser)
        {
            await _userValidator.ValidateAndThrowAsync(newUser);

            newUser.Password = _passwordHasher.HashPassword(newUser.Password);
            newUser.Roles.Add(new RoleDTO { Name = UserRoles.Regular });

            return await _userRepository.AddAsync(newUser);
        }

        public async Task<string> AuthenticateAsync(UserLoginDTO userLogin)
        {
            await _userLoginValidator.ValidateAndThrowAsync(userLogin);

            var storedUser = await ValidateUsername(userLogin.Username);
            ValidatePassword(userLogin, storedUser);

            return _tokenGenerator.GenerateToken(storedUser);
        }

        private void ValidatePassword(UserLoginDTO userLogin, UserDTO storedUser)
        {
            var validationResult = _passwordHasher.VerifyHashedPassword(
                storedUser.Password, userLogin.Password);

            if (validationResult == PasswordVerificationResult.Failed)
                throw new InvalidUserCredentialsException();
        }

        private async Task<UserDTO> ValidateUsername(string username)
        {
            var storedUser = await _userRepository.GetByUsernameIncludingRolesAsync(username);

            if (storedUser == null)
                throw new InvalidUserCredentialsException();

            return storedUser;
        }
    }
}
