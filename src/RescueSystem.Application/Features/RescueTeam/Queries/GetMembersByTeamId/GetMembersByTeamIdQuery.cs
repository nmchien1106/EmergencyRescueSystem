using MediatR;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetMembersByTeamId
{
    public class GetMembersByTeamIdQuery : IRequest<List<RescueTeamMemberDTO>>
    {
        public Guid TeamId { get; set; }
    }
}