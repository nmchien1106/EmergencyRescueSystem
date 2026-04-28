using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.Mission;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionById
{
    public class GetMissionByIdHandler(IMissionRepository missionRepository) : IRequestHandler<GetMissionByIdQuery, MissionDTO>
    {
        public async Task<MissionDTO> Handle(GetMissionByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = await missionRepository.GetByIdAsync(request.Id);
            if(mission == null)
            {
                throw new Exception("Mission not found");
            }

            return new MissionDTO
            {
                Id = mission.Id,
                RequestId = mission.RequestId,
                DispatcherId = mission.DispatcherId,
                RescueTeamId = mission.RescueTeamId,
                StartTime = mission.StartTime,
                EndTime = mission.EndTime,
                Status = mission.Status.ToString(),
                TeamName = mission.RescueTeam?.TeamName // tui lấy thêm tên đội luôn
            };

        }
    }
}
