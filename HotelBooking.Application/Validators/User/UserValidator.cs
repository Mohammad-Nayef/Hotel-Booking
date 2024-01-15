using FluentValidation;
using HotelBooking.Domain.Abstractions.Repositories;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Application.Validators.User
{
    internal class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(IUserRepository userRepository)
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .Length(UserConstants.MinNameLength, UserConstants.MaxNameLength)
                .Matches(UserConstants.NameRegex)
                .WithMessage("{PropertyName} has invalid format.");

            RuleFor(user => user.LastName)
                .NotNull()
                .Length(UserConstants.MinNameLength, UserConstants.MaxNameLength)
                .Matches(UserConstants.NameRegex);

            RuleFor(user => user.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(user => user.Username)
                .NotNull()
                .Length(UserConstants.MinUsernameLength, UserConstants.MaxUsernameLength)
                .Matches(UserConstants.UsernameRegex)
                .WithMessage("{PropertyName} has invalid format.")
                .MustAsync(async (username, cancellation) =>
                    !await userRepository.UsernameExistsAsync(username))
                .WithMessage("{PropertyName} already exists.");

            RuleFor(user => user.Password)
                .NotNull()
                .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength);
        }
    }
}
