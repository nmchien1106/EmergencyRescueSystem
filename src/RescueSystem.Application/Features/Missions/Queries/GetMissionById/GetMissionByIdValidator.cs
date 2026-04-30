using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionById
{
    public class GetMissionByIdValidator : AbstractValidator<GetMissionByIdQuery>
    {
        public GetMissionByIdValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Mission ID is required.")
           .Must(id => id != Guid.Empty).WithMessage("Mission ID must be a valid GUID.");
        }
    }
}
