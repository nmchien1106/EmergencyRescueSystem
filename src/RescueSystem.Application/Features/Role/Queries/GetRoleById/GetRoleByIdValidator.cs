using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Queries.GetAllRoles
{
    public class GetRoleByIdValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Role ID is required.")
           .Must(id => id != Guid.Empty).WithMessage("Role ID must be a valid GUID.");
        }
    }
}
