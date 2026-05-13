using MediatR;
using RescueSystem.Application.Features.Checklist;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.ChecklistItem.Queries.GetChecklistItems
{
    public class GetChecklistItemsQuery : IRequest<List<ChecklistItemDTO>>
    {
        public Guid ChecklistId { get; set; }
    }
}
