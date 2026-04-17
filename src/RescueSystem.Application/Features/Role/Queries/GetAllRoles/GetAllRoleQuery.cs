using MediatR;
using RescueSystem.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Queries.GetAllRoles
{
    public class GetAllRoleQuery : IRequest<List<RoleDTO>>
    {
        // empty
    }
}
