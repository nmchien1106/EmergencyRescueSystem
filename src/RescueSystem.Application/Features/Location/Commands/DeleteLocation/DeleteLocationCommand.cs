using MediatR;

namespace RescueSystem.Application.Features.Location.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
