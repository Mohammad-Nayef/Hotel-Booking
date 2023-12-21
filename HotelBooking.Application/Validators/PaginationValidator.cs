using FluentValidation;
using HotelBooking.Domain.Constants;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Validators
{
    internal class PaginationValidator : AbstractValidator<PaginationDTO>
    {
        public PaginationValidator()
        {
            RuleFor(pagination => pagination.PageNumber)
                .NotNull()
                .GreaterThanOrEqualTo(PaginationConstants.MinPageNumber);

            RuleFor(pagination => pagination.PageSize)
                .NotNull()
                .InclusiveBetween(
                    PaginationConstants.MinPageSize, PaginationConstants.MaxPageSize);
        }
    }
}
