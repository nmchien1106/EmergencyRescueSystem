using MediatR;
using RescueSystem.Application.DTOs.Location;

namespace RescueSystem.Application.Features.Location.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<LocationDTO>
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;
    }
}
