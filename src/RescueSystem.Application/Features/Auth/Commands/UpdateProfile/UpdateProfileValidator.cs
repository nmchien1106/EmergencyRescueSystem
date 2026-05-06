using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Auth.Commands.UpdateProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20);

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address!.Street)
                    .MaximumLength(255);

                RuleFor(x => x.Address!.City)
                    .MaximumLength(100);

                RuleFor(x => x.Address!.District)
                    .MaximumLength(100);

                RuleFor(x => x.Address!.GPS)
                    .MaximumLength(255);
            });
        }
    }
}
