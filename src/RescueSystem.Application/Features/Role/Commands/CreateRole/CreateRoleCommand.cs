using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Unit>
    {
        public string RoleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
