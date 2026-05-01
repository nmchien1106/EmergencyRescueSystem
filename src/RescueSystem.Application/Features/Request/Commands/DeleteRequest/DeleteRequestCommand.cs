using MediatR;

namespace RescueSystem.Application.Features.Request.Commands.DeleteRequest
{
    public class DeleteRequestCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
    }
}
