
using MediatR;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Repositories;

namespace RescueSystem.Application.Features.RescueTeam.Commands.RemoveMemberFromRescueTeam
{
    public class RemoveMemberFromRescueTeamHandler : IRequestHandler<RemoveMemberFromRescueTeamCommand, bool>
    {
        private readonly IRescueTeamRepository _rescueTeamRepository;
        public RemoveMemberFromRescueTeamHandler(IRescueTeamRepository rescueTeamRepository) {
            _rescueTeamRepository = rescueTeamRepository;
        }

        
        public async Task<bool> Handle(RemoveMemberFromRescueTeamCommand request, CancellationToken cancellationToken)
        {
            var removed = await _rescueTeamRepository.RemoveMemberAsync(request.TeamId, request.MemberId);

            if(!removed) {
                throw new NotFoundException("Member is not in this rescue team");
            }
            return true;
        }
    }
}