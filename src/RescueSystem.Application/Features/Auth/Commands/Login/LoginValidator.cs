using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RescueSystem.Application.Features.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc")
                .EmailAddress().WithMessage("Định dạng email không hợp lệ");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password là bắt buộc");
        }
    }
}
