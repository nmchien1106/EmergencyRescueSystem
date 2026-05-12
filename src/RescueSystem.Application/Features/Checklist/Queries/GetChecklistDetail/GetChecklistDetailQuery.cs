using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Checklist.Queries.GetChecklistDetail
{
    public class GetChecklistDetailQuery : IRequest<ChecklistDetailDTO>
    {
        public Guid Id { get; set; }
    }
}
