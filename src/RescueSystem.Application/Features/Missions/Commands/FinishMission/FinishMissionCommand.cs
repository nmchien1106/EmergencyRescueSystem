using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.FinishMission
{
    public class FinishMissionCommand : IRequest<bool>
    {
        public Guid MissionId { get; set; }
    }
}
