using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.Mission;
using RescueSystem.Domain.Entities;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionById
{
    public class GetMissionByIdQuery : IRequest<MissionDTO>
    {
        public Guid Id { get; set; }
    }
}
