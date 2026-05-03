using AutoMapper;
using MediatR;
using RescueSystem.Application.DTOs.Location;

namespace RescueSystem.Application.Features.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQuery : IRequest<List<LocationDTO>>
    {
    }
}
