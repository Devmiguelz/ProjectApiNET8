using FluentValidation;
using PruebaAnnarApi.Application.Dto.User;
using System.Data;

namespace PruebaAnnarApi.Validators
{
    public class UserCreateValidator: AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password cannot be short than 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character.");
        }
    }
}
