using MediatR;
using Microsoft.AspNetCore.Http;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.RescueTeam;
using RescueSystem.Application.Features.RescueTeam.Command.CreateRescueTeam;
using RescueSystem.Domain.Enums;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Application.Features.RescueTeam.Commands.CreateRescueTeam 
{
    public class CreateRescueTeamHandler: IRequestHandler<CreateRescueTeamCommand, RescueTeamDTO>
    {
        private readonly IRescueTeamRepository _rescueTeamRepository;

        public CreateRescueTeamHandler(IRescueTeamRepository rescueTeamRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
        }

        public async Task<RescueTeamDTO> Handle(CreateRescueTeamCommand request, CancellationToken cancellationToken)
        {
            var rescueTeam = new RescueSystem.Domain.Entities.RescueTeam
            {
                TeamName = request.TeamName,
                TeamLeaderId = request.TeamLeaderId,
                BaseLocationId = request.BaseLocationId,
                Status = TeamStatus.UNAVAILABLE
            };

            var createdTeam = await _rescueTeamRepository.CreateAsync(rescueTeam);
            if(!createdTeam)
            {
                throw new Exception("Không thể tạo đội cứu hộ");
            }
            return new RescueTeamDTO
            {
                Id =rescueTeam.Id,
                TeamName = rescueTeam.TeamName,
                Status = rescueTeam.Status.ToString()
            };
        }
    }
}