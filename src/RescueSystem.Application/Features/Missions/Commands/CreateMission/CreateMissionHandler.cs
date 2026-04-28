using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Application.Features.Missions.Commands.CreateMission
{
    public class CreateMissionHandler : IRequestHandler<CreateMissionCommand, Guid>
    {
        private readonly IMissionRepository _missionRepository;
        public CreateMissionHandler(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }
        public async Task<Guid> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = new Mission
            {
                Id = Guid.NewGuid(),
                RequestId = request.RequestId,
                DispatcherId = request.DispatcherId,
                RescueTeamId = request.RescueTeamId,
                StartTime = DateTime.UtcNow,
                Status = Domain.Enums.MissionStatus.ASSIGNED,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _missionRepository.AddAsync(mission);
            return mission.Id;
        }
    }
}
