using FluentValidation;

namespace RescueSystem.Application.Features.RescueTeam.Commands.AddMemberToRescueTeam
{
    public class AddMemberToRescueTeamValidator : AbstractValidator<AddMemberToRescueTeamCommand>
    {
        public AddMemberToRescueTeamValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage("TeamId is required");
            RuleFor(x => x.MemberId).NotEmpty().WithMessage("MemberId is required");
        }
    }
}