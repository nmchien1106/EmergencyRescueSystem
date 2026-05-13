using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Checklist.Commands.UpdateChecklist
{
    public class UpdateChecklistValidator : AbstractValidator<UpdateChecklistCommand>
    {
        public UpdateChecklistValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty()
               .MaximumLength(256);
        }
    }
}
