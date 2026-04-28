using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.Mission;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionById
{
    public class GetMissionByIdQuery : IRequest<MissionDTO>
    {
        public Guid Id { get; set; }
    }
}
