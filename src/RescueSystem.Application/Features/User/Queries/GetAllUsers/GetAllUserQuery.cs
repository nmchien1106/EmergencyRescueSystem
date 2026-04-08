using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.User;

namespace RescueSystem.Application.Features.User.Queries.GetAllUser
{
    
    public class GetAllUserQuery : IRequest<List<UserDTO>>
    {
        // No parameters needed for this query, as we want to get all users
    }
}
