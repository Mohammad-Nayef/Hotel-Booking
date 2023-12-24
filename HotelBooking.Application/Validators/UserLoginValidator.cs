using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Username)
                .NotNull()
                .Matches(UserConstants.UsernameRegex)
                .WithMessage("{PropertyName} has invalid format.");

            RuleFor(user => user.Password)
                .NotNull()
                .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength);
        }
    }
}
