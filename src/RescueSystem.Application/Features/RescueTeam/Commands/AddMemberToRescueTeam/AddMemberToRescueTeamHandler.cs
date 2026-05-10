using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;

namespace RescueSystem.Application.Features.RescueTeam.Commands.AddMemberToRescueTeam
{
    public class AddMemberToRescueTeamHandler:IRequestHandler<AddMemberToRescueTeamCommand,bool>
     {
        private readonly IRescueTeamRepository _rescueTeamRepository;

        public AddMemberToRescueTeamHandler(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }

        public async Task<bool> Handle(AddMemberToRescueTeamCommand request, CancellationToken cancellationToken)
        
        {
            return await _rescueTeamRepository.AddMemberAsync(request.TeamId, request.MemberId);
        }
     }
}