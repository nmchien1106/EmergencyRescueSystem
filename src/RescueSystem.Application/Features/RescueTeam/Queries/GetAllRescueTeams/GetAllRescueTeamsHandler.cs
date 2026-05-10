
using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetAllRescueTeams
{
    public class GetAllRescueTeamsHandler : IRequestHandler<GetAllRescueTeamsQuery, List<RescueTeamDTO>>
    {
        
        private readonly IRescueTeamRepository _rescueTeamRepository;
        public GetAllRescueTeamsHandler(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }
        
        public async Task<List<RescueTeamDTO>> Handle(GetAllRescueTeamsQuery request, CancellationToken cancellationToken)
        {
            var rescueTeams = await _rescueTeamRepository.GetAllAsync();
            return rescueTeams.Select(x => new RescueTeamDTO
            {
                Id = x.Id,
                TeamName = x.TeamName,
                Status = x.Status.ToString()
            }).ToList();
        }
    }
}