using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.AbortMission
{
    public class AbortMissionHandler : IRequestHandler<AbortMissionCommand, bool>
    {
        private readonly IMissionRepository _missionRepository;

        public AbortMissionHandler(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public async Task<bool> Handle(AbortMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _missionRepository.GetByIdAsync(request.MissionId);
            if (mission == null)
            {
                throw new Exception("Không tìm thấy nhiệm vụ!");
            }
            // khoong cho abort khi nvu đã hoàn thành hoặc đã bị hủy
            if (mission.Status == MissionStatus.COMPLETED
                || mission.Status == MissionStatus.ABORTED)
            {
                return false;
            }

            mission.Status = MissionStatus.ABORTED;
            mission.UpdatedAt = DateTime.UtcNow.AddHours(7);
            mission.EndTime = DateTime.UtcNow.AddHours(7);

            await _missionRepository.UpdateAsync(mission);
            return true;
        }
    }
}
