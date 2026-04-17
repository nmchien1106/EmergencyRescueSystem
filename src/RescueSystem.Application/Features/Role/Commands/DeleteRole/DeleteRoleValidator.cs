using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Role Id is required");
        }
    }
}
