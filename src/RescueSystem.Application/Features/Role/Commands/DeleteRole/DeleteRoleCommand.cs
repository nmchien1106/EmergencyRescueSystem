using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
