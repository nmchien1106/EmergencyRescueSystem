using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;

namespace RescueSystem.Application.Features.RescueTeam.Commands.DeleteRescueTeam
{
    public class DeleteRescueTeamHandler : IRequestHandler<DeleteRescueTeamCommand, bool>
    {

        private readonly IRescueTeamRepository _rescueTeamRepository;
        public DeleteRescueTeamHandler(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }



        public async Task<bool> Handle(DeleteRescueTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _rescueTeamRepository.GetByIdAsync(request.Id);
            if (team == null)
            {
                throw new KeyNotFoundException("Rescue team not found");
            }

            return await _rescueTeamRepository.DeleteAsync(team);
        }
    }
}