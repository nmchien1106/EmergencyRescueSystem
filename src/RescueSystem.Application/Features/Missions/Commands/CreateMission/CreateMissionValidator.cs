using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.CreateMission
{
    public class CreateMissionValidator : AbstractValidator<CreateMissionCommand>
    {
        public CreateMissionValidator()
        {
        }
    }
}