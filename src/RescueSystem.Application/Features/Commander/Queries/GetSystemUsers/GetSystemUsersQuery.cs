using MediatR;
using RescueSystem.Application.DTOs.Commander;
using RescueSystem.Application.DTOs.User;
using System.Collections.Generic;

namespace RescueSystem.Application.Features.User.Queries.GetSystemUsers
{
    public class GetSystemUsersQuery : IRequest<List<UserSystemDTO>>
    {
        public string? Search { get; set; }
        public string? Role { get; set; }
    }
}