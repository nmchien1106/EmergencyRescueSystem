using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Missions.Commands.UpdateMission
{
    public class UpdateMissionHandler : IRequestHandler<UpdateMissionCommand, bool>
    {
        private readonly IMissionRepository _missionRepository;
        public UpdateMissionHandler(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public async Task<bool> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _missionRepository.GetByIdAsync(request.MissionId); // laays nhiem vu
            if (mission == null)
            {
                throw new Exception("Không tìm thấy nhiệm vụ!");
            } // neu ko tim thay
            
            if (mission.Status == MissionStatus.COMPLETED)
            {
                return false; // khoong cho update nhieem vu da hoan thanh
            }

            // chuyen trang thai
            bool isValidTransition = false;

            if (mission.Status == MissionStatus.ASSIGNED
                && request.Status == MissionStatus.EN_ROUTE)
                isValidTransition = true;

            else if (mission.Status == MissionStatus.EN_ROUTE
                && request.Status == MissionStatus.ON_SITE)
                isValidTransition = true;

            else if (mission.Status == MissionStatus.ON_SITE
                && request.Status == MissionStatus.IN_PROGRESS)
                isValidTransition = true;

            else if (mission.Status == MissionStatus.IN_PROGRESS
                && request.Status == MissionStatus.COMPLETED)
                isValidTransition = true;

            else if (mission.Status == MissionStatus.ABORTED)
                isValidTransition = true;

            if (!isValidTransition)
                return false; // chuyen trang thai khong hop le

            // cap nhat cais trang thai
            mission.Status = request.Status;

            // cap nhat thoi gian 
            if (request.Status == MissionStatus.COMPLETED
                || request.Status == MissionStatus.ABORTED)
            {
                mission.EndTime = DateTime.UtcNow;
            }
            // thoi gian update
            mission.UpdatedAt = DateTime.UtcNow;

            await _missionRepository.UpdateAsync(mission);
            return true;
        }
    }
}
