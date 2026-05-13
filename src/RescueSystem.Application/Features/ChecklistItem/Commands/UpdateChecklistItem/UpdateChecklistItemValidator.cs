using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.ChecklistItem.Commands.UpdateChecklistItem
{
    public class UpdateChecklistItemValidator : AbstractValidator<UpdateChecklistItemCommand>
    {
        public UpdateChecklistItemValidator()
        {
            RuleFor(x => x.Description)
               .NotEmpty()
               .MaximumLength(256);
        }
    }
}
