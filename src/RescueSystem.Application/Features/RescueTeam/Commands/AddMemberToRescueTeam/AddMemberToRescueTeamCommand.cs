using MediatR;

namespace RescueSystem.Application.Features.RescueTeam.Commands.AddMemberToRescueTeam
{
    public class AddMemberToRescueTeamCommand : IRequest<bool>
    {
        public Guid TeamId { get; set; }
        public Guid MemberId { get; set; }
    }
}