using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.AbortMission
{
    public class AbortMissionCommand : IRequest<bool>
    {
        public Guid MissionId { get; set; }
    }
}
