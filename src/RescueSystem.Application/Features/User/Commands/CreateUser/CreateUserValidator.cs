using FluentValidation;
using System;
using System.Linq;
using RescueSystem.Application.Features.User.Commands;

namespace RescueSystem.Application.Features.User.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200).WithMessage("Full name must not exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MinimumLength(3).WithMessage("UserName must be at least 3 characters.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{7,15}$")
                .WithMessage("Phone number must contain only digits and may start with '+' (7-15 digits).");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Matches(@"(?=.*[A-Za-z])(?=.*\d)").WithMessage("Password must contain letters and numbers.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Roles must be provided.")
                .Must(r => r.Any()).WithMessage("At least one role must be specified.")
                .ForEach(r => r.NotEmpty().WithMessage("Role value must not be empty."));

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address must not exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.Avatar)
                .Must(u => string.IsNullOrWhiteSpace(u) || Uri.IsWellFormedUriString(u, UriKind.Absolute))
                .WithMessage("Avatar must be a valid absolute URI when provided.");
        }
    }
}
