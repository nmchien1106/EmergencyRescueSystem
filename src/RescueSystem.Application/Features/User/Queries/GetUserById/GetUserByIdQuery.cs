using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.User;

namespace RescueSystem.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}
