using FluentValidation;

namespace RescueSystem.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id là bắt buộc");

            RuleFor(x => x.Latitude)
                .NotEmpty().WithMessage("Latitude là bắt buộc");

            RuleFor(x => x.Longitude)
                .NotEmpty().WithMessage("Longitude là bắt buộc");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address là bắt buộc")
                .MaximumLength(500).WithMessage("Address không được vượt quá 500 ký tự");

            RuleFor(x => x.Landmark)
                .MaximumLength(255).WithMessage("Landmark không được vượt quá 255 ký tự")
                .When(x => !string.IsNullOrWhiteSpace(x.Landmark));
        }
    }
}
