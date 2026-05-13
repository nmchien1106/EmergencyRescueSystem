using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.ChecklistItem.Commands.CreateChecklistItem
{
    public class CreateChecklistItemValidator : AbstractValidator<CreateChecklistItemCommand>
    {
        public CreateChecklistItemValidator()
        {
            RuleFor(x => x.Description)
               .NotEmpty()
               .MaximumLength(256);
        }
    }
}
