using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RescueSystem.Application.Features.Auth.Queries.Profile
{
    public class ProfileValidator : AbstractValidator<ProfileQuery>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId là bắt buộc");
        }
    }
}
