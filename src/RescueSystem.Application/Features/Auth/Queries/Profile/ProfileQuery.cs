using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.DTOs.Auth;

namespace RescueSystem.Application.Features.Auth.Queries.Profile
{
    public class ProfileQuery : IRequest<ProfileResponse>
    {
        public string UserId { get; set; }
    }
}
