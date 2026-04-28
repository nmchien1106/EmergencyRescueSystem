using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.CreateMission
{
    public class CreateMissionCommand : IRequest<Guid>
    {
        public Guid RequestId { get; set; }

        public Guid DispatcherId { get; set; }
        public Guid RescueTeamId { get; set; }
    }
}
