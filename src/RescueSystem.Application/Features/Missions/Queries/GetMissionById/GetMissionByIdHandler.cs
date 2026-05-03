using MediatR;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.Dispatcher;
using RescueSystem.Application.DTOs.Mission;
using RescueSystem.Application.DTOs.Request;
using RescueSystem.Application.DTOs.RescueTeam;
using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionById
{
    public class GetMissionByIdHandler(IMissionRepository missionRepository) : IRequestHandler<GetMissionByIdQuery, MissionDetailDTO>
    {
        public async Task<MissionDetailDTO> Handle(GetMissionByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = await missionRepository.GetByIdAsync(request.Id);
            if (mission == null)
            {
                throw new Exception("Mission not found");
            }

            return new MissionDetailDTO
            {
                Id = mission.Id,

                Request = mission.Request == null ? null : new RequestDTO
                {
                    Id = mission.Request.Id,
                    Description = mission.Request.Description,
                    EmergencyType = mission.Request.EmergencyType,
                    Priority = mission.Request.Priority
                },

                RescueTeam = mission.RescueTeam == null ? null : new RescueTeamDTO
                {
                    Id = mission.RescueTeam.Id,
                    TeamName = mission.RescueTeam.TeamName,
                    Status = mission.RescueTeam.Status.ToString()
                },

                Dispatcher = mission.Dispatcher == null ? null : new DispatcherDTO
                {
                    Id = mission.Dispatcher.Id,
                    Name = mission.Dispatcher.FullName,
                    Email = mission.Dispatcher.Email
                },
                StartTime = mission.StartTime.AddHours(7),
                EndTime = mission.EndTime.HasValue ? mission.EndTime.Value.AddHours(7) : null,
                CreateAt = mission.CreatedAt.AddHours(7),
                UpdateAt = mission.UpdatedAt.AddHours(7)
            };
        }
    }
}
