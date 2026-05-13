using MediatR;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetAllRescueTeams
{
    public class GetAllRescueTeamsQuery : IRequest<List<RescueTeamDTO>>
    {

    }
}