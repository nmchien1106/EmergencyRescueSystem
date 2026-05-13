using MediatR;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetRescueTeamById{
    public class GetRescueTeamByIdHandler : IRequestHandler<GetRescueTeamByIdQuery, RescueTeamDTO>
    {
        private readonly IRescueTeamRepository _rescueTeamRepository;

        public GetRescueTeamByIdHandler(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }

        public async Task<RescueTeamDTO> Handle(GetRescueTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _rescueTeamRepository.GetByIdAsync(request.Id);
            if (team == null)
            {
                throw new NotFoundException("Rescue team not found");
            }
            return new RescueTeamDTO
            {
                Id = team.Id,
                TeamName = team.TeamName,
                Status = team.Status.ToString()
            };
        }
    }
}