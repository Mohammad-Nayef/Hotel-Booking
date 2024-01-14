using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models.User;

namespace HotelBooking.Application.Validators.User
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
