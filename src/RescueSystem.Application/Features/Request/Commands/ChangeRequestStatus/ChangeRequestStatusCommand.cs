using MediatR;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Request.Commands.ChangeRequestStatus
{
    public class ChangeRequestStatusCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
        public RequestStatus NewStatus { get; set; }
    }
}