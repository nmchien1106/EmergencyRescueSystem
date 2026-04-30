using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Commands.UpdateMission
{
    public class UpdateMissionValidator : AbstractValidator<UpdateMissionCommand>
    {
        public UpdateMissionValidator()
        {
            RuleFor(x => x.MissionId)
                .NotEmpty().When(x => x.MissionId != Guid.Empty)
                .WithMessage("Yêu cầu phải có Id nhiệm vụ")
                .Must(id => id != Guid.Empty).WithMessage("Id nhiệm vụ phải là một GUID hợp lệ.");
        }
    }
}
