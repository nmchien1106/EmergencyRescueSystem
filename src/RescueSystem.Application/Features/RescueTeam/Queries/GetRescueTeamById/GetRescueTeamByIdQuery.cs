using MediatR;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetRescueTeamById
{
    public class GetRescueTeamByIdQuery : IRequest<RescueTeamDTO>
    {
        public Guid Id { get; set; }
    }
}