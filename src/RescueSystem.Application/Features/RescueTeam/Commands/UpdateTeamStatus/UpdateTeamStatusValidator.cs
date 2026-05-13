using FluentValidation;
namespace RescueSystem.Application.Features.RescueTeam.Commands.UpdateTeamStatus
{
    public class UpdateTeamStatusValidator : AbstractValidator<UpdateTeamStatusCommand>
    {
        public UpdateTeamStatusValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage("Team ID is required");
            RuleFor(x => x.NewStatus).IsInEnum().WithMessage("Invalid team status");
        }
    }
}

