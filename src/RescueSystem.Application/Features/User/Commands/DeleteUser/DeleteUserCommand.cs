using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RescueSystem.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
