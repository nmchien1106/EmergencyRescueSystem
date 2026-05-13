using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RescueSystem.Application.Features.Checklist.Commands.UpdateChecklist
{
    public class UpdateChecklistCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Title { get; set; }
    }
}
