using MediatR;
using RescueSystem.Application.DTOs.Location;

namespace RescueSystem.Application.Features.Location.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<LocationDTO>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;
    }
}
