using MediatR;
using RescueSystem.Application.DTOs.Mission;
using System;
using System.Collections.Generic;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionHistory
{
    public class GetMissionHistoryQuery : IRequest<IEnumerable<MissionHistoryDTO>>
    {
        public Guid MissionId { get; set; }
    }
}