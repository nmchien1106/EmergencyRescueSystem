using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.FinishMission
{
    public class FinishMissionCommand : IRequest<bool>
    {
        public Guid MissionId { get; set; }
        public Guid ChangedById { get; set; } // Người thực hiện thay đổi
        public string Note { get; set; } = string.Empty; // Ghi chú khi hoàn thành
    }
}
