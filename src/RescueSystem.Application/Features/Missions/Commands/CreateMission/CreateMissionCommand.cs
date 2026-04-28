using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.CreateMission
{
    public class CreateMissionCommand : IRequest<Guid>
    {
        public Guid RequestId { get; set; } // lien ket toi recueRequest

        public Guid DispatcherId { get; set; } // id nguoi dieu phoi giao nhiem vu

        public Guid RescueTeamId { get; set; } // id doi cuu ho duoc giao nhiem vu
    }
}
