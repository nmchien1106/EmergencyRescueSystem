using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Checklist.Commands.CreateChecklist
{
    public class CreateChecklistCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public Guid MissionId { get; set; }
    }
}
