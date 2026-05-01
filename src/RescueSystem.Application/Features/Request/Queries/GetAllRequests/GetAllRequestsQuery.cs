using MediatR;
using RescueSystem.Application.DTOs.Common;
using RescueSystem.Application.DTOs.Request;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Request.Queries.GetAllRequests
{
    public class GetAllRequestsQuery : IRequest<PagedResult<RequestDTO>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public RequestStatus? Status { get; set; }
        public Priority? Priority { get; set; }
        public EmergencyType? EmergencyType { get; set; }
        public string? SortBy { get; set; } = "submittedTime";
    }
}
