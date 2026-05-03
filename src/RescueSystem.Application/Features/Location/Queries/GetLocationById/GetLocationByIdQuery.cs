using MediatR;
using RescueSystem.Application.DTOs.Location;

namespace RescueSystem.Application.Features.Location.Queries.GetLocationById
{
    public class GetLocationByIdQuery : IRequest<LocationDTO>
    {
        public Guid Id { get; set; }
    }
}
