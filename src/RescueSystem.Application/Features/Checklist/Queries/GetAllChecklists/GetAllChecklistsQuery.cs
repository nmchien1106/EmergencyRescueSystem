using MediatR;
using RescueSystem.Application.DTOs.Checklist;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Checklist.Queries.GetAllChecklists
{
    public class GetAllChecklistsQuery : IRequest<List<ChecklistDTO>>
    {

    }
}
