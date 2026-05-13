using MediatR;

namespace RescueSystem.Application.Features.RescueTeam.Commands.DeleteRescueTeam
{
    public class DeleteRescueTeamCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}