using MediatR;
using RescueSystem.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Queries.GetAllRoles
{
    public class GetRoleByIdQuery : IRequest<RoleDTO>
    {
        public Guid Id { get; set; }
    }
}
