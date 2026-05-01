using MediatR;
using Microsoft.AspNetCore.Http;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Request.Commands.UpdateRequest
{
    public class UpdateRequestCommand : IRequest<RescueRequest>
    {
        public Guid RequestId { get; set; }
        public Guid? UserId { get; set; }
        public EmergencyType EmergencyType { get; set; }
        public Priority Priority { get; set; }
        public RequestStatus Status { get; set; }
        public Guid LocationId { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<IFormFile>? Files { get; set; }
    }
}
