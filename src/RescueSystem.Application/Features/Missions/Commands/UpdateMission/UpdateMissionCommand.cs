using MediatR;
using RescueSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.UpdateMission
{
    public class UpdateMissionCommand : IRequest<bool>
    {
        public Guid MissionId { get; set; }
        public MissionStatus Status { get; set; }
        public Guid ChangedById { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
