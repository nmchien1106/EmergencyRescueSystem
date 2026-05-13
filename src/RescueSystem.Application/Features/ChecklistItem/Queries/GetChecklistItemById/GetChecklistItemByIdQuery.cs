using MediatR;
using RescueSystem.Application.Features.Checklist;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.ChecklistItem.Queries.GetChecklistItemById
{
    public class GetChecklistItemByIdQuery : IRequest<ChecklistItemDTO>
    {
        public Guid Id { get; set; }
    }
}
