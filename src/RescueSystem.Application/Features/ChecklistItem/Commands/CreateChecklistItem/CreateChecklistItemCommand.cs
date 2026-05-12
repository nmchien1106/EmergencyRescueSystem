using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RescueSystem.Application.Features.ChecklistItem.Commands.CreateChecklistItem
{
    public class CreateChecklistItemCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid ChecklistId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
