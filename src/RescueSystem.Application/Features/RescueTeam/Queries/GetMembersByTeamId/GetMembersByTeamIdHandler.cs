using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.RescueTeam;

namespace RescueSystem.Application.Features.RescueTeam.Queries.GetMembersByTeamId
{
    public class GetMembersByTeamIdCommand : IRequestHandler<GetMembersByTeamIdQuery, List<RescueTeamMemberDTO>>
    {
        private readonly IRescueTeamRepository _rescueTeamRepository;

        public GetMembersByTeamIdCommand(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }

        public async Task<List<RescueTeamMemberDTO>> Handle(GetMembersByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var members = await _rescueTeamRepository.GetMembersByTeamIdAsync(request.TeamId);
            return members.Select(m => new RescueTeamMemberDTO
            {
                Id = m.Id,
                FullName = m.FullName,
                Email = m.Email ?? string.Empty,
                PhoneNumber = m.PhoneNumber,
                Avatar = m.Avatar,
                IsActive = m.IsActive
            }).ToList();
        }
    }
}